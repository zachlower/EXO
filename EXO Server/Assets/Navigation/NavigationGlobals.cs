using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NavigationGlobals {

    /* includes all enums for room and navigation purposes
     * 
     */

    /* types that will dictate how server presents room and how players interact with it */
	public enum RoomType
    {
        Empty,
        Combat,
        Healing,
        Loot,
        End
    }

    
}
