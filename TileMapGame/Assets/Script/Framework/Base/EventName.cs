using UnityEngine;
using System.Collections;

public class EventUI
{
	public uint EVENT_UI_SHOW_LOADING_ALPHA   = 0x0000001;
	public uint EVENT_UI_HIDE_LOADING_ALPHA   = 0x0000002;
}
public class EventInput
{
	public uint InteractMapTap = 0x0100001;
}
public class GameEvent
{
	public static EventInput m_EventInput = new EventInput();
}