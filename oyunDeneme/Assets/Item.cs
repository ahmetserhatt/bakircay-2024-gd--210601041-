using UnityEngine;

public class Items : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float initialY; // Nesnenin baþlangýçtaki Y (yükseklik) pozisyonu

    // Baþlangýçta Rigidbody bileþenini al ve baþlangýç yüksekliðini kaydet
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y;
    }

    // Nesne týklandýðýnda çalýþýr
    private void OnMouseDown()
    {
        DisableGravity();
        CaptureScreenPointAndOffset();
    }

    // Nesne sürüklenirken çalýþýr
    private void OnMouseDrag()
    {
        MoveObjectWithCursor();
    }

    // Nesne býrakýldýðýnda çalýþýr
    private void OnMouseUp()
    {
        EnableGravity();
    }

    // Yerçekimini devre dýþý býrakýr
    private void DisableGravity()
    {
        rb.useGravity = false;
    }

    // Yerçekimini etkinleþtirir
    private void EnableGravity()
    {
        rb.useGravity = true;
    }

    // Ekran noktasý ve sürükleme ofsetini yakalar
    private void CaptureScreenPointAndOffset()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Nesneyi fare imlecine göre hareket ettirir
    private void MoveObjectWithCursor()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
        targetPosition.y = initialY; // Y pozisyonunu sabit tut
        rb.MovePosition(targetPosition);
    }
}
