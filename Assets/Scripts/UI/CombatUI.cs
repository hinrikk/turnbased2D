using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CombatUI : MonoBehaviour
{
    private Button moveButton;
    private Button attackButton;
    private Button baseAttackButton;
    private Button rangeAttackButton;
    private Button skipTurn;
    VisualElement skillContainer;
    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        moveButton = root.Q<Button>("player-move");
        attackButton = root.Q<Button>("player-attack");
        baseAttackButton = root.Q<Button>("player-base-attack");
        rangeAttackButton = root.Q<Button>("player-range-attack");
        skipTurn = root.Q<Button>("player-skip-turn");

        attackButton.clicked += OnAttackPressed;
        moveButton.clicked += OnMovePressed;
        baseAttackButton.clicked += OnBaseAttackPressed;
        rangeAttackButton.clicked += OnRangeAttackPressed;
        skipTurn.clicked += OnSkipTurnPressed;

        skillContainer =  root.Q<VisualElement>("SkillContainer");

    }

    void Start()
    {
        Controller.Instance.OnPlayerModeChanged += RefreshUI;
        RefreshUI();
    }

    void RefreshUI()
    {
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is Enemy)// For debugging; Enemies not yet playable
            return;


        // Update Skills in UI dynamically
        skillContainer.Clear();
        foreach (Skill skill in currentUnit.skills)
        {
            Button button = new Button();
            button.text = skill.skillName;
            button.clicked += () =>
            {
                OnSkillButtonPressed(skill);
            };
            skillContainer.Add(button);
        }

        if (Controller.Instance.mode == PlayerMode.Move)
        {
            moveButton.SetEnabled(false);
            attackButton.SetEnabled(true);
            baseAttackButton.SetEnabled(false);
            rangeAttackButton.SetEnabled(false);
        }
        else if (Controller.Instance.mode == PlayerMode.Attack)
        {
            moveButton.SetEnabled(true);
            attackButton.SetEnabled(false);
            rangeAttackButton.SetEnabled(true);
            baseAttackButton.SetEnabled(true);
        }

        TurnUI.Instance.RefreshTurnUI();
    }

    void OnBaseAttackPressed()
    {
        Debug.Log("Base Attack");
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if(currentUnit is CharacterMovement player)
        {
            Controller.Instance.ChangeAttackType(PlayerAttackType.BaseAttack);
        }
    }

    void OnRangeAttackPressed()
    {
        Debug.Log("Range Attack");
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is CharacterMovement player)
        {
            Controller.Instance.ChangeAttackType(PlayerAttackType.RangeAttack);
        }
    }

    void OnSkipTurnPressed()
    {
        Debug.Log("Skip Turn");
        TurnManager.Instance.EndCurrentTurn();
    }

    void OnAttackPressed()
    {
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is CharacterMovement player)
        {
            Controller.Instance.SwitchAttackMode(PlayerMode.Attack);
        }
    }

    void OnMovePressed()
    {
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is CharacterMovement player)
        {
            Controller.Instance.SwitchAttackMode(PlayerMode.Move);
        }
    }

    void OnSkillButtonPressed(Skill skill)
    {
        Debug.Log($"{skill.skillName}");
        Controller.Instance.SwitchPlayerSkill(skill);
        Controller.Instance.SwitchAttackMode(PlayerMode.Cast);
    }
}
