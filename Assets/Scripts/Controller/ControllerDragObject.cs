using UnityEditor.Timeline;
using UnityEngine;

public class ControllerDragObject : MonoBehaviour
{
    #region Private Variables
    private Vector3 m_Offset;
    private float m_ZCooordinate;
    #endregion

    #region Select Object Variables
    Rigidbody rigidbody;
    private LayerMask layer;
    private bool canAttachable = false;
    bool isMove = false;
    [Header ("References")]
    [Space]
    [SerializeField] MeshRenderer attachMesh;
    [SerializeField] Material detectMaterial;
    [SerializeField] Material unDetectMaterial;
    [SerializeField] Transform attachPoint;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isMove)
        {
            float timer = 0.0f;
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, attachPoint.position, 5f * Time.deltaTime);
            if (timer >= 1f)
            {
                isMove = false;
            }
        }
    }
    #endregion

    #region Select-Drag Functions
    private void OnMouseDown()
    {
        m_ZCooordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        m_Offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition();

        HoldRotation();

        FindObject(layer, attachPoint);
    }

    private void OnMouseUp()
    {
        ReleaseObject();
        if (canAttachable)
        {
            isMove = true;
            transform.parent = attachPoint;
            //transform.position = attachPoint.position;
            transform.position = Vector3.Lerp(transform.position, attachPoint.position, .01f * Time.deltaTime);
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            GameManager.Instance.pistonCount++;
        }
        else
        {
            transform.parent = null;
            isMove = false;
            rigidbody.constraints = RigidbodyConstraints.None;
            GameManager.Instance.pistonCount--;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = m_ZCooordinate;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        SameObjectAttach(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        canAttachable = false;
        attachMesh.material = unDetectMaterial;
    }
    #endregion

    #region Find Select Object
    void FindObject(LayerMask layer, Transform attachPoint)
    {
        layer = gameObject.layer;
        attachMesh.enabled = true;
    }

    void ReleaseObject()
    {
        attachMesh.enabled = false;
    }

    void SameObjectAttach(GameObject other)
    {
        if (gameObject.layer == other.layer)
        {
            attachMesh.material = detectMaterial;
            canAttachable = true;
        }
    }

    void HoldRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, attachPoint.rotation, 1f);
    }
    #endregion
}
