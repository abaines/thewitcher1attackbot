#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.
#MaxThreadsPerHotkey 2

#Persistent

;SetTimer, CloseMailWarnings, 500
;return

;CloseMailWarnings:
	;MsgBox, Hello2
;return

;Loop
;{
;Tooltip, %A_Cursor%
;SplashTextOn,,, A MsgBox is about to appear.12312312
;Sleep 1000
;SplashTextOff
;return
;}




;SoundPlay *-1
;SoundPlay, %A_WinDir%\Media\ding.wav

;SoundBeep ;
;SoundBeep, 750, 500
;Esc::Exitapp

;MsgBox , , "hiya", "text", 500
;MsgBox "hello"

;a::
;SplashTextOn,,, A MsgBox is about to appear.
;Sleep 300
;SplashTextOff
;return

















CoordMode, ToolTip, Screen

cursor_id = 32512|32513|32514|32515|32516|32640|32641|32642|32643|32644|32645|32646|32648|32649|32650|32651
cursor_id32512?name = ARROW
cursor_id32513?name = IBEAM
cursor_id32514?name = WAIT
cursor_id32515?name = CROSS
cursor_id32516?name = UPARROW
cursor_id32640?name = SIZE
cursor_id32641?name = ICON
cursor_id32642?name = SIZENWSE
cursor_id32643?name = SIZENESW
cursor_id32644?name = SIZEWE
cursor_id32645?name = SIZENS
cursor_id32646?name = SIZEALL
cursor_id32648?name = NO
cursor_id32649?name = HAND
cursor_id32650?name = APPSTARTING
cursor_id32651?name = HELP

loop, parse, cursor_id, |
{
	h_cursor := DllCall( "LoadCursor", "uint", 0, "uint", A_LoopField )
	if ( !ErrorLevel and h_cursor )
		h_cursor%h_cursor%?name := cursor_id%A_LoopField%?name
}

VarSetCapacity( ci, 20, 0 )

SetTimer, timer_MonitorCursor, 10
return

timer_MonitorCursor:
	ci := Chr( 20 )
	success := DllCall( "GetCursorInfo", "uint", &ci )

	h_cursor := *( &ci+8 )+( *( &ci+9 ) << 8 )+( *( &ci+10 ) << 16 )+( *( &ci+11 ) << 24 )
	
	if h_cursor%h_cursor%?name=
		ToolTip, cursor = UNKNOWN, 10, 10
	else
		ToolTip, % "cursor = " h_cursor%h_cursor%?name, 10, 10
		
	o := Ord(ci)
	SplashTextOn,,, cursor3: %o%
	Sleep 1300
	SplashTextOff
return



































SetTimer, delayed_action, 1000

delayed_action:
{
	if (MyNumber)
	{
		;cc := CursorCheck()
		SplashTextOn,,, cursor: %cc%
		Sleep 1300
		SplashTextOff
		;MsgBox, delayed_action
		;WinMaximize Figure 1
		return
	}
}

global MyNumber := true


























Home::
SetTimer, Send_W, 0
return


End::
SetTimer, Send_w, off
send {w up}
return

Send_w:
Send {w down}
sleep 100
return


