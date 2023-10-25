using System.Security.Claims;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WorkSpaceInit : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject m_initUI;
    [SerializeField] private GameObject m_mainUI;
    [Header("Init")]
    [SerializeField] private GameObject m_horizontalLine;
    [SerializeField] private Material m_lineMat;
    [SerializeField] private LineRenderer m_lineRen;
    [SerializeField] private Transform m_cursor;
    [SerializeField] private WorkSpace m_workSpaceToSpawn;

    private Camera cam;
    private Vector3 point1 = new();
    private Vector3 point2 = new();
    private Vector3 point3 = new();
    private bool isLockedHorizontal = false;
    public void toogleLockHorizontal()
    {
        isLockedHorizontal = !isLockedHorizontal;
    }

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("workspace", LoadSceneMode.Additive);
        }
        if (!isLockedHorizontal)
        {
            m_horizontalLine.transform.position = new Vector3(cam.transform.position.x, m_horizontalLine.transform.position.y, cam.transform.position.z);
        }
        else
        {
            Ray ray = new()
            {
                origin = cam.transform.position,
                direction = cam.transform.forward
            };
            Physics.Raycast(ray, out RaycastHit hit);
            if (hit.collider != null)
            {
                m_cursor.position = hit.point;
            }
        }
    }
    public void SpawnWorkSpace()
    {
        m_initUI.gameObject.SetActive(false);
        m_mainUI.gameObject.SetActive(true);
        m_cursor.gameObject.SetActive(false);
        //m_lineRen.gameObject.SetActive(false);
        m_horizontalLine.gameObject.SetActive(false);

        var workspace = Instantiate(m_workSpaceToSpawn, (point1+point3)*0.5f, Quaternion.identity);
        workspace.transform.LookAt((point3+point2)*0.5f);
        workspace.Height = (point1 - point2).magnitude;
        workspace.Width = (point2 - point3).magnitude;
    }
    public void OnPointerDown(BaseEventData data)
    {
        Debug.Log("try cast ray");
        Ray ray = new()
        {
            origin = cam.transform.position,
            direction = cam.transform.forward
        };
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null)
        {
            Debug.Log(hit.point);
            if (point1 == Vector3.zero)
            {
                point1 = hit.point;
                m_lineRen.SetPosition(0, point1);
            }
            else if (point2 == Vector3.zero)
            {
                point2 = hit.point;
                m_lineRen.SetPosition(1, point2);
            }
            else
            {
                point3 = hit.point;
                m_lineRen.SetPosition(2, point3);
            }
        }
    }
    public void OnDrag(BaseEventData data)
    {
        if (isLockedHorizontal) return;
        PointerEventData touch = data as PointerEventData;
        m_horizontalLine.transform.position += new Vector3(0, touch.delta.y * Time.deltaTime, 0);
    }
}
