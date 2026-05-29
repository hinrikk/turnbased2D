using UnityEngine;
using UnityEngine.UIElements;

public class TurnUI : MonoBehaviour
{
    VisualElement turnContainer;
    public static TurnUI Instance;
    public VisualTreeAsset turnEntryTemplate;

    void Awake()
    {
        Instance = this;
        var root = GetComponent<UIDocument>().rootVisualElement;
        turnContainer = root.Q<VisualElement>("turn-container");
    }

    public void RefreshTurnUI()
    {
        turnContainer.Clear();

        foreach (Unit unit in TurnManager.Instance.units)
        {
            VisualElement entry = turnEntryTemplate.Instantiate();
            Label name = entry.Q<Label>("character-name");
            Label hp = entry.Q<Label>("character-hp");
            Label mana = entry.Q<Label>("character-mana");

            name.text = unit.name;
            hp.text = $"HP: {unit.health}";
            mana.text = "TODO!";

            turnContainer.Add(entry);
        }
    }


}
