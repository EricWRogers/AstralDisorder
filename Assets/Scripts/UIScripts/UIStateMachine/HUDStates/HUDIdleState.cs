using OmnicatLabs.CharacterControllers;

public class HUDIdleState : UINullState
{
    private UIStateMachineController inputController;
    public MouseLook mouseLook;

    public override void OnStateEnter(UIStateMachineController controller)
    {
        base.OnStateEnter(controller);
        inputController = controller;
    }

    public override void OnStateUpdate(UIStateMachineController controller)
    {
        base.OnStateUpdate(controller);
    }
}
