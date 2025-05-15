using UnityEngine;

namespace CodeBase.Logic.Infrastructure
{
    public class GameEntryPoint : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            _game.Launch();
        }
    }
}