using Helpers;

namespace States.UIStates
{
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
            StateDictionary.Add(menuState.stateKey, menuState);
            StateDictionary.Add(gameState.stateKey, gameState);
        }

        public override void SetMainStateNone()
        {
            base.SetMainStateNone();
            mainState = Enums.UIStates.None;
            isTransitioningMainState = false;
        }

        public override void SetSubStateNone()
        {
            base.SetSubStateNone();
            subState = Enums.UIStates.None;
            isTransitioningSubState = false;
        }

    }
}