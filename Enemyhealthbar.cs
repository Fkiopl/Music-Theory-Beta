using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private int maxHP;
    private int currentHP;

    private GameObject barRoot;
    private RectTransform fillRect;
    private Camera cam;

    public void Setup(int hp)
    {
        maxHP = hp;
        currentHP = hp;
        cam = Camera.main;
        CreateBar();
    }

    void CreateBar()
    {
        // Create world space canvas above enemy
        barRoot = new GameObject("EnemyHPBar");
        barRoot.transform.SetParent(transform);
        barRoot.transform.localPosition = new Vector3(0, 0.7f, 0);
        barRoot.transform.localScale = new Vector3(0.01f, 0.01f, 1f);

        Canvas c = barRoot.AddComponent<Canvas>();
        c.renderMode = RenderMode.WorldSpace;
        RectTransform rt = barRoot.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(60, 8);

        // Black background
        GameObject bg = new GameObject("BG");
        bg.transform.SetParent(barRoot.transform, false);
        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = Color.black;
        RectTransform bgRt = bg.GetComponent<RectTransform>();
        bgRt.anchorMin = Vector2.zero;
        bgRt.anchorMax = Vector2.one;
        bgRt.offsetMin = Vector2.zero;
        bgRt.offsetMax = Vector2.zero;

        // Red fill
        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(barRoot.transform, false);
        Image fillImg = fill.AddComponent<Image>();
        fillImg.color = Color.red;
        fillRect = fill.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = new Vector2(1f, 1f);
        fillRect.offsetMin = new Vector2(1, 1);
        fillRect.offsetMax = new Vector2(-1, -1);
    }

    public void UpdateBar(int current)
    {
        currentHP = current;
        if (fillRect == null) return;
        float ratio = (float)currentHP / maxHP;
        fillRect.anchorMax = new Vector2(ratio, 1f);
    }

    void Update()
    {
        // Always face camera
        if (barRoot != null && cam != null)
            barRoot.transform.rotation = cam.transform.rotation;
    }
}
