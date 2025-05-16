using UnityEngine;

namespace CodeBase.Logic.Infrastructure.Bootstrap
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