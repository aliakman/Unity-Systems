using State;
using Helpers;

public class UIStateHandler : StateHandler<Enums.UIStates>
{
    private UIMenuState _menuState;
    public UIMenuState menuState { get { return _menuState != null ? _menuState : _menuState = new UIMenuState(Enums.UIStates.Menu, this); } }

    private UIGameState _gameState;
    public UIGameState gameState { get { return _gameState != null ? _gameState : _gameState = new UIGameState(Enums.UIStates.Game, this); } }


    private void Start()
    {
        InitStateHandler();
    }

    private void InitStateHandler()
    {
        States.Add(menuState.StateKey, menuState);
        States.Add(gameState.StateKey, gameState);
    }

}