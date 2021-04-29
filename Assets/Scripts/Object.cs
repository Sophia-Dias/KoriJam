using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public class Object : MonoBehaviour
{
  Animator animator;
  Rigidbody rb;
  Material outline;
  private void OnDrawGizmos()
  {
    Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
    Gizmos.color = Color.red;
    Gizmos.DrawWireMesh(mesh, 0, transform.position, transform.rotation, transform.localScale);
  }

  private void Awake()
  {
    animator = gameObject.GetComponent<Animator>();
    outline = gameObject.GetComponent<MeshRenderer>().materials[1];
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
