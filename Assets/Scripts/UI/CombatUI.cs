using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    private Button combatButton;
    private Button baseAttackButton;
    private Button rangeAttackButton;
    void Awake()
    {
        var root =
            GetComponent<UIDocument>()
            .rootVisualElement;

        combatButton = root.Q<Button>("switch-player-mode");
        baseAttackButton = root.Q<Button>("player-base-attack");
        rangeAttackButton = root.Q<Button>("player-range-attack");

        combatButton.clicked += OnCombatPressed;
        baseAttackButton.clicked += OnBaseAttackPressed;
        rangeAttackButton.clicked += OnRangeAttackPressed;
    }

    void OnCombatPressed()
    {
        PlayerMovement.Instance.ToggleAttackMode();
    }

    void OnBaseAttackPressed()
    {
        Debug.Log("Base Attack");
        PlayerMovement.Instance.ChangeAttackType(PlayerAttackType.BaseAttack);
    }

    void OnRangeAttackPressed()
    {
        Debug.Log("Range Attack");
        PlayerMovement.Instance.ChangeAttackType(PlayerAttackType.RangeAttack);
    }
}
