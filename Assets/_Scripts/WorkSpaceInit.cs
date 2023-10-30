using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkSpaceInit : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject m_initUI;
    [SerializeField] private GameObject m_lineUI;
    [SerializeField] private GameObject m_mainUI;
    [SerializeField] private TextMeshProUGUI m_TMP;
    [Header("Init")]
    [SerializeField] private GameObject m_horizontalPlane;
    [SerializeField] private Material m_lineMat;
    [SerializeField] private LineRenderer m_lineRen;
    [SerializeField] private Transform m_cursor;
    [SerializeField] private WorkSpace m_workSpaceToSpawn;

    private Camera cam;
    private int currentPoint = 0;
    private bool isLockedHorizontal = false;
    public void toogleLockHorizontal()
    {
        isLockedHorizontal = !isLockedHorizontal;
        if (isLockedHorizontal)
        {
            m_lineUI.SetActive(false);
            m_horizontalPlane.SetActive(true);
            m_horizontalPlane.transform.position = cam.transform.position;
            m_TMP.text = "Draw table sides";
        }
        else
        {
            m_lineUI.SetActive(true);
            m_horizontalPlane.SetActive(false);
            m_TMP.text = "Align line to table surface";
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (isLockedHorizontal)
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
                if (currentPoint <= 2)
                {
                    m_lineRen.SetPosition(currentPoint, hit.point);
                }
            }
        }
    }
    public void SpawnWorkSpace()
    {
        m_initUI.gameObject.SetActive(false);
        m_mainUI.gameObject.SetActive(true);
        m_cursor.gameObject.SetActive(false);
        m_lineRen.gameObject.SetActive(false);
        m_horizontalPlane.gameObject.SetActive(false);

        var point1 = m_lineRen.GetPosition(0);
        var point2 = m_lineRen.GetPosition(1);
        var point3 = m_lineRen.GetPosition(2);


        var workspace = Instantiate(m_workSpaceToSpawn, (point1+point3)*0.5f, Quaternion.identity);
        workspace.transform.LookAt((point3+point2)*0.5f);
        workspace.Height = (point1 - point2).magnitude;
        workspace.Width = (point2 - point3).magnitude;

        var middleDiag = (point1 + point3) * 0.5f;
        CLUHandler.SpawnPos =new Vector3(middleDiag.x, middleDiag.y + 1, middleDiag.z);
    }
    public void OnPointerDown(BaseEventData data)
    {
        if (!isLockedHorizontal) return;
        NextPoint();
    }
    private void NextPoint()
    {
        currentPoint++;
        if (currentPoint > 2) return;
        m_lineRen.positionCount = currentPoint+1;
    }
    public void PrevPoint()
    {
        currentPoint--;
    }
}
