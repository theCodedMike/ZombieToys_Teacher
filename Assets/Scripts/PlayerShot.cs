using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("发射点位置")]
    public Transform firePoint;
    [Header("2次发射间期")]
    public float shootInterval;
    [Header("发射曲线")]
    public AnimationCurve curve;
    [Header("发射的射线振幅")]
    public float rayHeight;
    [Header("射击时的遮罩")]
    public LayerMask mask;


    private LineRenderer lineRenderer;

    private float timer;
    private Ray shootRay;
    private Vector3 endPoint;
    private float effectDisplayTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = firePoint.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootInterval)
        {
            timer = 0;
            Shoot();
        }

        if (timer > shootInterval * effectDisplayTime)
        {
            DisableEffect();
        }
    }

    void Shoot()
    {
        lineRenderer.enabled = true;
        shootRay.origin = firePoint.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay, out var hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
        {
            endPoint = hit.point;
            if (hit.transform.CompareTag("Enemy"))
            {
                print("enemy hit...");
                hit.transform.GetComponent<Enemy>().Damage();
            }
        }

        //lineRenderer.positionCount = 2;
        //lineRenderer.SetPosition(0, firePoint.position);
        //lineRenderer.SetPosition(1, endPoint);
        lineRenderer.positionCount = curve.keys.Length;
        Vector3 rayDir = endPoint - firePoint.position;
        for (int i = 0; i < curve.keys.Length; i++)
        {
            Keyframe kf = curve.keys[i];
            Vector3 pos = firePoint.position + rayDir * kf.time;
            pos += Vector3.up * kf.value * rayHeight;
            lineRenderer.SetPosition(i, pos);
        }
    }

    void DisableEffect()
    {
        lineRenderer.enabled = false;
    }
}
