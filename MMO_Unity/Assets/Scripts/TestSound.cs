using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip audioClip2;

    private bool IsToggle = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsToggle)
        {
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
            IsToggle = !IsToggle;
        }
        else
        {
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
            IsToggle = !IsToggle;
        }
    }
}