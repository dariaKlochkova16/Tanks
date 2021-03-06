﻿using System;

namespace DKFramework
{
    public class WinManager : CoreComponentBase
    {
        public event EventHandler<string> GameEnd;
        public WinManager(Core core) : base(core)
        {
            _core.DiedGameObject += new EventHandler(EndTest);
        }

        private void EndTest(object sender, EventArgs e)
        {
            GameObject gameObject = (GameObject)sender;
            switch(gameObject.NameType)
            {
                case ObjectType.Player:
                    if (Core.Instance.FindElement(ObjectType.Player) == null)
                        GameEnd(this, "Вы проиграли");
                    break;
                case ObjectType.Enemy:
                    if (Core.Instance.FindElement(ObjectType.Enemy) == null)
                        GameEnd(this, "Вы выйграли");
                    break;
                case ObjectType.Base:
                    GameEnd(this, "Вы проиграли");
                    break;
            }

        }
    }
}
