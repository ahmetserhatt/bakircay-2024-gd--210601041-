using UnityEngine;

public class Items : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float initialY; // Nesnenin ba�lang��taki Y (y�kseklik) pozisyonu

    // Ba�lang��ta Rigidbody bile�enini al ve ba�lang�� y�ksekli�ini kaydet
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y;
    }

    // Nesne t�kland���nda �al���r
    private void OnMouseDown()
    {
        DisableGravity();
        CaptureScreenPointAndOffset();
    }

    // Nesne s�r�klenirken �al���r
    private void OnMouseDrag()
    {
        MoveObjectWithCursor();
    }

    // Nesne b�rak�ld���nda �al���r
    private void OnMouseUp()
    {
        EnableGravity();
    }

    // Yer�ekimini devre d��� b�rak�r
    private void DisableGravity()
    {
        rb.useGravity = false;
    }

    // Yer�ekimini etkinle�tirir
    private void EnableGravity()
    {
        rb.useGravity = true;
    }

    // Ekran noktas� ve s�r�kleme ofsetini yakalar
    private void CaptureScreenPointAndOffset()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Nesneyi fare imlecine g�re hareket ettirir
    private void MoveObjectWithCursor()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        targetPosition.y = initialY; // Y pozisyonunu sabit tut
        rb.MovePosition(targetPosition);
    }
}
