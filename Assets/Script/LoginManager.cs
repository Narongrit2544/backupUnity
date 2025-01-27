using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_Text loginStatusText;

    void Start()
    {
        // ถ้าเกมถูกเปิดจาก deep link ตอนเริ่ม ให้ตรวจสอบด้วย
        if (!string.IsNullOrEmpty(Application.absoluteURL))
        {
            ProcessDeepLink(Application.absoluteURL);
        }
        // ลงทะเบียน callback เมื่อรับ deep link
        Application.deepLinkActivated += ProcessDeepLink;
    }

    void ProcessDeepLink(string url)
    {
        Debug.Log("Deep Link URL: " + url);
        // สมมติว่า URL ที่ได้รับมีรูปแบบ: mygame://login?access_token=XYZ...
        if (url.StartsWith("mygame://login"))
        {
            // (เพิ่มเติม: คุณสามารถแยกเอา access_token ออกมาได้ถ้าจำเป็น)
            loginStatusText.text = "ล็อกอินสำเร็จแล้ว!";
        }
    }
}
