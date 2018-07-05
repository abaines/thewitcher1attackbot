#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
#MaxThreadsPerHotkey 2
#Persistent
#SingleInstance force
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

toggleRun := false

; when home key is pressed, start Send_w timer
Home::
SetTimer, Send_W, 0
return




; when end key is pressed, end Send_w timer
End::
SetTimer, Send_w, off
send {w up}
toggleRun := false
return

; when w key is pressed, end Send_w timer
~w::
SetTimer, Send_w, off
;send {w up}
toggleRun := false
return

; when s key is pressed, end Send_w timer
~s::
SetTimer, Send_w, off
send {w up}
toggleRun := false
return




; send the w key for 100 ms
Send_w:
Send {w down}
sleep 100
return


XButton1::
{
	;MsgBox, Hello World
	if (toggleRun)
	{
		SetTimer, Send_w, off
		send {w up}
		toggleRun := false
	}
	else
	{
		SetTimer, Send_W, 0
		toggleRun := true
	}
}
