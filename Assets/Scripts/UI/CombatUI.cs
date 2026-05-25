using UnityEngine;
using UnityEngine.UIElements;

public class CombatUI : MonoBehaviour
{
    private Button combatButton;
    private Button baseAttackButton;
    private Button rangeAttackButton;
    private Button skipTurn;
    void Awake()
    {
        var root =
            GetComponent<UIDocument>()
            .rootVisualElement;

        combatButton = root.Q<Button>("switch-player-mode");
        baseAttackButton = root.Q<Button>("player-base-attack");
        rangeAttackButton = root.Q<Button>("player-range-attack");
        skipTurn = root.Q<Button>("player-skip-turn");

        combatButton.clicked += OnCombatPressed;
        baseAttackButton.clicked += OnBaseAttackPressed;
        rangeAttackButton.clicked += OnRangeAttackPressed;
        skipTurn.clicked += OnSkipTurnPressed;
    }

    void OnCombatPressed()
    {
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is PlayerMovement player)
        {
            player.ToggleAttackMode();
        }
    }

    void OnBaseAttackPressed()
    {
        Debug.Log("Base Attack");
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if(currentUnit is PlayerMovement player)
        {
            player.ChangeAttackType(PlayerAttackType.BaseAttack);
        }
    }

    void OnRangeAttackPressed()
    {
        Debug.Log("Range Attack");
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is PlayerMovement player)
        {
            player.ChangeAttackType(PlayerAttackType.RangeAttack);
        }
    }

    void OnSkipTurnPressed()
    {
        Debug.Log("Skip Turn");
        TurnManager.Instance.EndCurrentTurn();
    }
}
