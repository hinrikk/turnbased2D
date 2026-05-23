using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    private Button combatButton;
    void Awake()
    {
        var root =
            GetComponent<UIDocument>()
            .rootVisualElement;

        combatButton =
            root.Q<Button>("switch-player-mode");

        combatButton.clicked += OnCombatPressed;
    }

    void OnCombatPressed()
    {
        PlayerMovement.Instance.ToggleAttackMode();
    }
}
