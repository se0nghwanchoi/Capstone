using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.Interaction.Toolkit;

public class FireValve : MonoBehaviour
{
    private XRSimpleInteractable simpleInteractable;
    private bool Used = false;
    private string url = "http://localhost/Capstone24/UnitActs.php";
    //private string url = "http://211.250.192.52:8080/Capstone24/UnitActs.php";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 예를 들어, 숫자1 키를 누르면 종료
        {
            StartCoroutine(FireValveInteract(Used));
        }
    }

    private void Awake()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();

        if (simpleInteractable == null)
        {
            Debug.LogError("필수 컴포넌트가 누락되었습니다!");
            return;
        }

        // Replace selectEntered and selectExited with hoverEntered and hoverExited for simple interactions
        simpleInteractable.selectEntered.AddListener(StartInteraction);
        simpleInteractable.selectExited.AddListener(EndInteraction);
    }

    private void OnDestroy()
    {
        simpleInteractable.selectEntered.RemoveListener(StartInteraction);
        simpleInteractable.selectExited.RemoveListener(EndInteraction);
    }

    private void StartInteraction(SelectEnterEventArgs args)
    {
        Debug.Log("상호작용 시작");
        Used = true;
    }

    private void EndInteraction(SelectExitEventArgs args)
    {
        Debug.Log($"Interaction with FireValve: {true}");
        //StartCoroutine(TowelInteracts(totalInteractionTime, Used));

    }

    private IEnumerator FireValveInteract(bool usedStatus)
    {
        int recordID = PlayerPrefs.GetInt("RecordID");
        WWWForm form = new WWWForm();
        form.AddField("recordID", recordID);
        form.AddField("doCode", 5);
        //form.AddField("interactionTime", 0);
        form.AddField("useStatus", usedStatus ? "1" : "0");

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Capstone24/UnitActs.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error while sending interaction time: {www.error}");
            }
            else
            {
                Debug.Log("Interaction time sent successfully");
            }
        }
    }
}
