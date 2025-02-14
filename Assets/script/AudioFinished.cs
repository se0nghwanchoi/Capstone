using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFinished : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject arrow; // 화살표 GameObject

    void Start()
    {
        arrow.SetActive(false); // 시작 시 화살표 비활성화
        Invoke("StartAudioCheck", 15f); // 15초 후 StartAudioCheck 메소드를 호출하여 오디오 재생
    }

    void StartAudioCheck()
    {
        audioSource.Play(); // 오디오 재생 시작
        StartCoroutine(WaitForAudioEnd()); // 오디오가 끝나기를 기다리는 코루틴 시작
    }

    IEnumerator WaitForAudioEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying); // 오디오가 재생되는 동안 대기
        arrow.SetActive(true); // 오디오 재생 끝나면 화살표 활성화
    }
}