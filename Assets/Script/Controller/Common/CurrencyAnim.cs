using UnityEngine;
using System.Collections;

public class CurrencyAnim : MonoBehaviour
{
    public float delay = 5.0f;
    public float repeatRate = 5.0f;
    public float animInterval = 1.0f;
    public UISpriteAnimation iconAnim = null;
    public UISpriteAnimation starAnim = null;

    private bool m_Active = false;

    private float time = 0.0f;
    // Use this for initialization
    void Start()
    {
        iconAnim.gameObject.SetActive(false);
        starAnim.gameObject.SetActive(false);
        InvokeRepeating("PlayIconAnim",delay,repeatRate);
    }
    
    // Update is called once per frame
    void Update()
    {
        iconAnim.gameObject.SetActive(iconAnim.isPlaying&&m_Active);
        starAnim.gameObject.SetActive(starAnim.isPlaying&&m_Active);
    }

    private void PlayIconAnim()
    {
        m_Active = true;
        iconAnim.Play();
        Invoke("PlayStarAnim",animInterval);
    }

    private void PlayStarAnim()
    {
        starAnim.Play();
    }
}
