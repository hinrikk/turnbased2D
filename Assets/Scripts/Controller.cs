using UnityEngine;
using UnityEngine.InputSystem;
using System;


public enum PlayerMode
{
    Move,
    Attack,
    Cast,
}
public enum PlayerAttackType
{
    BaseAttack,
    RangeAttack
}

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public PlayerMode mode = PlayerMode.Move;
    public PlayerAttackType attackType = PlayerAttackType.BaseAttack;
    public Skill playerSkill;
    public bool isPlayerTurn;
    public event Action OnPlayerModeChanged;

    void Awake()
    {
        Instance = this;
    }


    void Update()
    {

        //
        Unit currentUnit = TurnManager.Instance.getCurrentUnit();
        if (currentUnit is not CharacterMovement)
        {
            isPlayerTurn = false;
            return;
        }

        isPlayerTurn = true;
        CharacterMovement currentControllableUnit = currentUnit as CharacterMovement;


        if (currentControllableUnit.moving)
            return;

        // Get Mouse Input
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        world.z = 0;
        Vector3Int targetCell = Vector3Int.FloorToInt(world);
        Vector3Int startCell = Vector3Int.RoundToInt(currentUnit.transform.position);


        if (mode == PlayerMode.Move)
        {
            currentControllableUnit.HandleMove(startCell, targetCell);
        }
        if (mode == PlayerMode.Attack)
        {
            PathPreview.Instance.ClearPath();
            currentControllableUnit.HandleAttack(startCell, targetCell, attackType);
        }
        if(mode == PlayerMode.Cast)
        {
            PathPreview.Instance.ClearPath();
            currentControllableUnit.HandleSkill(currentUnit, targetCell, playerSkill);
        }

    }

    public void SwitchAttackMode(PlayerMode m)
    {
        mode = m;
        OnPlayerModeChanged?.Invoke();
    }

    public void ChangeAttackType(PlayerAttackType buttonType)
    {
        attackType = buttonType;

    }

    public void SwitchPlayerSkill(Skill s)
    {
        playerSkill = s;
        OnPlayerModeChanged?.Invoke();
    }

}
