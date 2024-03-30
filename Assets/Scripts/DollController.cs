using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    public float minTimer, maxTimer;
    public bool isGreenLight = true;

    public Animator animator;
    public readonly string greenLightAnim = "GreenLight";

    public Transform shotPoint;
    public GameObject bulletPrefab;

    private AudioSource audioSource;
    public AudioClip redLightSfx, greenLightSfx, shootSfx;

    public bool hasShot;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLightCoroutine());
    }

    IEnumerator ChangeLightCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minTimer, maxTimer));

        if (isGreenLight)
        {
            audioSource.PlayOneShot(redLightSfx);
            animator.SetBool(greenLightAnim, false);
            yield return new WaitForSeconds(0.7f);
            isGreenLight = false;
            print("Lampu Merah, gak boleh jalan bro!");
        }
        else
        {
            audioSource.PlayOneShot(greenLightSfx);
            isGreenLight = true;
            animator.SetBool(greenLightAnim, true);
            print("Lampu Hijau, udah boleh jalan bro!");
        }

        StartCoroutine(ChangeLightCoroutine());
    }

    public void ShootPlayer(Transform playerTarget)
    {
        if (hasShot) return;
        audioSource.PlayOneShot(shootSfx);
        GameObject bulletGO = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        bulletGO.GetComponent<BulletMovement>().playerTarget = playerTarget;
        hasShot = true;
    }
}
