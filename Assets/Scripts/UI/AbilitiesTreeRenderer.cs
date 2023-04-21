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

            // Получаем RectTransform каждого элемента
            var rectTransform1 = panels[id].GetComponent<RectTransform>();
            var rectTransform2 = panels[linkedId].GetComponent<RectTransform>();

            // Создаем abilityTransictionPrefab и устанавливаем его родителем abilitiesCanvas
            var transictionObj = Instantiate(abilityTransictionPrefab, abilitiesTransictionCanvas.transform);

            // Находим центр между элементами и переводим его в координаты экрана
            Vector3 center = (rectTransform1.position + rectTransform2.position) / 2;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(center);

            // Переводим координаты экрана в локальные координаты abilitiesCanvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(abilitiesCanvas.GetComponent<RectTransform>(), screenPoint, Camera.main, out Vector2 localPoint);

            // Устанавливаем позицию объекта abilityTransictionPrefab
            transictionObj.transform.localPosition = localPoint;

            // Находим расстояние между двумя элементами
            float distance = Vector2.Distance(rectTransform1.anchoredPosition, rectTransform2.anchoredPosition);

            // Устанавливаем ширину и высоту объекта AbilityPanelTransition
            transictionObj.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, transictionObj.GetComponent<RectTransform>().sizeDelta.y);

            // Находим угол между двумя элементами
            float angle = Mathf.Atan2(rectTransform2.anchoredPosition.y - rectTransform1.anchoredPosition.y, rectTransform2.anchoredPosition.x - rectTransform1.anchoredPosition.x) * Mathf.Rad2Deg;

            // Поворачиваем объект AbilityPanelTransition
            transictionObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}