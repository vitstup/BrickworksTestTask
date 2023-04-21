using UnityEngine;

public class AbilitiesTreeRenderer : MonoBehaviour
{

    [SerializeField] private Canvas abilitiesCanvas;
    [SerializeField] private Canvas abilitiesTransictionCanvas;

    [SerializeField] private AbilityPanel[] panels;
    [SerializeField] private GameObject abilityTransictionPrefab;

    private void Awake()
    {
        MainManager.RenderTreeEvent.AddListener(RenderAbilitiesTree);
    }
    private void RenderAbilitiesTree(AbilitiesTree tree)
    {
        if (tree.abilities.Count != panels.Length) Debug.LogWarning("Wrong amount of panels");
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].UpdateInfo(tree.abilities[i]);
            GenerateTransitions(tree, i);
        }
    }

    private void GenerateTransitions(AbilitiesTree tree, int id)
    {
        var linked = tree.abilities[id].linkedTo;
        if (linked == null || linked.Length == 0) return;
        for (int i = 0; i < linked.Length; i++)
        {
            int linkedId = tree.GetAbilityId(linked[i]);

            // create abilityTransictionPrefab between panels[linkedId] and panels[id]

            // �������� RectTransform ������� ��������
            var rectTransform1 = panels[id].GetComponent<RectTransform>();
            var rectTransform2 = panels[linkedId].GetComponent<RectTransform>();

            // ������� abilityTransictionPrefab � ������������� ��� ��������� abilitiesCanvas
            var transictionObj = Instantiate(abilityTransictionPrefab, abilitiesTransictionCanvas.transform);

            // ������� ����� ����� ���������� � ��������� ��� � ���������� ������
            Vector3 center = (rectTransform1.position + rectTransform2.position) / 2;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);

            // ��������� ���������� ������ � ��������� ���������� abilitiesCanvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(abilitiesCanvas.GetComponent<RectTransform>(), screenPoint, Camera.main, out Vector2 localPoint);

            // ������������� ������� ������� abilityTransictionPrefab
            transictionObj.transform.localPosition = localPoint;

            // ������� ���������� ����� ����� ����������
            float distance = Vector2.Distance(rectTransform1.anchoredPosition, rectTransform2.anchoredPosition);

            // ������������� ������ � ������ ������� AbilityPanelTransition
            transictionObj.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, transictionObj.GetComponent<RectTransform>().sizeDelta.y);

            // ������� ���� ����� ����� ����������
            float angle = Mathf.Atan2(rectTransform2.anchoredPosition.y - rectTransform1.anchoredPosition.y, rectTransform2.anchoredPosition.x - rectTransform1.anchoredPosition.x) * Mathf.Rad2Deg;

            // ������������ ������ AbilityPanelTransition
            transictionObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}