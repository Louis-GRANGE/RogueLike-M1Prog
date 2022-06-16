using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [HideInInspector]
    public PauseMenu pauseMenu;
    [HideInInspector]
    public LooseMenu looseMenu;
}
