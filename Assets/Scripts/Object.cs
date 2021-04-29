using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Object : MonoBehaviour
{
  [SerializeField] private GameObject baseMesh;
  [SerializeField] private GameObject outlineMesh;
  [SerializeField] private AnimatorController animatorController;

  Animator animator;
  Rigidbody rb;
  Material outline;

  private void OnDrawGizmos()
  {
    Mesh mesh = baseMesh.GetComponent<MeshFilter>().sharedMesh;
    Gizmos.color = Color.red;
    Gizmos.DrawWireMesh(mesh, 0, baseMesh.transform.position, baseMesh.transform.rotation, baseMesh.transform.localScale);
  }

  private void Awake()
  {
    outline = outlineMesh.GetComponent<MeshRenderer>().material;
    animator = outlineMesh.AddComponent<Animator>();
    animator.runtimeAnimatorController = animatorController;
    rb = gameObject.GetComponent<Rigidbody>();
    rb.useGravity = false;
    rb.isKinematic = true;
  }

  private void OnEnable() => PlayerEcholocaization.Instance.allObjects.Add(this);
  private void OnDisable() => PlayerEcholocaization.Instance.allObjects.Remove(this);

  public void MakeVisible(Vector3 clapPosition)
  {
    outline.SetVector("_ClapPosition", clapPosition);
    animator.SetTrigger("MakeVisible");
  }
}
