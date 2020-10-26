using Cinemachine.Utility;
using UnityEngine;

public class ShootingAtScript : MonoBehaviour
{
    float speed = 10.0f;
    private LineRenderer m_lineRenderer = null;
    Vector3 linePointing;
    private void Start()
    {
        linePointing = Vector3.zero;
        m_lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(200, 200, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layer_mask = ~LayerMask.GetMask("TerrainCollider");

        if (Physics.Raycast(ray, out hit, 100, layer_mask)) 
        {


            linePointing = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            linePointing.y = gameObject.transform.position.y;
            Vector3 localCord = gameObject.transform.InverseTransformPoint(linePointing);



            float angle = Mathf.Atan(localCord.x / localCord.z) * Mathf.Rad2Deg;


            if (Mathf.Abs(angle) < 80 && localCord.z > 0)
            {
                m_lineRenderer.SetPosition(1, localCord);
                m_lineRenderer.SetPosition(2, localCord * 100);
            }


        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    }


}
