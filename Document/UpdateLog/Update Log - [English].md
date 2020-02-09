Fix: represents a bug that was fixed
Modify: represents some modify
Add: represents a newly added function

<br/>

<br/>

<br/>


【v1.0.0.1】
1. [Fix]Tool's setting data, not saved
2. [Modify]Darken some text in a dark theme——Thanks：踏长安
3. [Modify]The main interface window can be dragged——Thanks：playwhite、Linzh
4. [Fix]In the English language, in the bug interface, the text of the update times is sometimes Chinese

<br/>

【v1.0.0.0】

1. [Fix]In the collaborative mode, if you never enter the bug interface after opening the software, if a bug is synchronized at this time, the software will crash.
2. [Modify]In collaboration mode, the text "This feature is being tested" has been modified.
3. [Fix]The version number of the dark theme is changed to v1.0.0.0
4. [Fix]The color of the sync button does not change when switching themes.
5. [Fix]In the collaborative mode, if user A changes the name of the bug, when syncing to user B, the message "Synchronization successful" will not be displayed, and the related synchronization log will not be displayed.
6. [Modify]Changed collaboration logic so nothing appears when there are no files to sync
7. [Modify]In collaborative mode, if there is nothing to sync, the sync icon will not turn up
8. [Fix]In the collaborative mode, if a bug file or a record file is deleted, it is not synchronized
9. [Fix]In collaborative mode, the pictures are not synchronized because the picture file is occupied.
10. [Fix]In collaborative mode, when adding a record, the update number are not synchronized
11. [Modify]When exporting Excel files, the bugs are sorted from the back to the back according to the creation time
12. [Fix]If the IsDelete field of a bug is true, it can still be exported to Excel
13. [Fix]If the IsDelete field of a bug is true, it can still be displayed in the related bug
14. [Fix]In collaborative mode, if user A creates more than two bugs at a time, user B's software will crash (because the PageSystem.Insert method exceeds the index)
15. [Fix]In the collaborative mode, the bug file is deleted. After copying the bug file to the bug folder, there is no synchronous display, but the bug has been read.
16. [Fix]When creating a bug, if you are searching for text, and the new bug does not contain search text, and it is on the last page, and the last page has less than 10 bugs, the software will crash (because the PageSystem.GetBugIndexInCurrentPage method exceeds the index)

<br/>

【v0.0.0.9】

1. [Add]Add "Open File" button in the Image interface
2. [Add]Collaborative Mode function completed (beta)
3. [Add]Can adjust the size of the software interface
4. [Modify]"Priority: Medium" icon for cat dark theme changed to brown
5. [Modify]The "Language" column of the setting interface is displayed as "English" and "中文"
6. [Modify]Sort of "related bugs", sorted in the order of creation time from back to front
7. [Add]Tool ("Repair Project" and "Convert Project")
8. [Fix]When opening a project with a dark theme, the checkbox for BugItem will turn white (it will appear normal after switching skins)

<br/>

【v0.0.0.8】

1. [Add]Dark theme
2. [Add]Cat dark theme (DLC only)
3. [Add]If the folder to be created already exists when creating the project, let the name of the folder we want to create become [xxxx / folder (1)]
4. [Fix]Page numbers are not updated when deleting or adding bugs
5. [Modify]BugId, record Id, and picture Id are all numbered in the form [year, month, day, hour, minute, second, millisecond] (number changed from int to long)
6. [Modify]When exporting an Excel file, the name of Excel is [item name-year, month, day, minute, second, millisecond.xlsx]
7. [Fix]Unable to delete the log file in the project file

<br/>

【v0.0.0.7】

1. [Fix]When searching for bugs, the total number of page numbers does not change
2. [Add]Delete the bug prompt interface, limit the length of the bug name——Thanks：原浩成
3. [Fix]When creating a project, if the project name has only one space, it can be created successfully, but the project file is not generated correctly——Thanks：原浩成
4. [Fix]After opening the project, close the project, and then create a new project, the contents of the previous project will be displayed
5. [Fix]If the cat theme is selected, when you open the software, the bear theme will be displayed first, and then the cat theme——Thanks：小木
6. [Modify]When opening the .bugs file, make a judgment. If it is not a project file, do not open the project and report an error.
7. [Add]In the project file, create a "Backup" folder, which is used to store the backup Bug.json file
8. [Fix]On the BugItem, hold down the left mouse button, and then click the right mouse button. The right-click menu of BugItem will display multiple——Thanks：原浩成

<br/>

【v0.0.0.6】

1. [Add]Cat light theme (DLC only)
2. [Fix]SolveBugTime in BugUi, which can be modified after pressing the Tab key
3. [Add]Save & read user's changes in the settings interface
4. [Add]When clicking the close button in the bug list interface, return to the open project interface

<br/>

【v0.0.0.5】

1. [Add]Display error details when the software crashes (error interface)
2. [Modify]Delete the second confirmation of chat——Thanks：小木
3. [Fix]Can't delete chat history when replying to bug
4. [Modify]Minimize the software when clicking on the icon in the taskbar——Thanks：瓜指导

<br/>

【v0.0.0.4】

1. [Add]In the bug list, double-click the bug to enter the edit page——Thanks：瓜指导、小木
2. [Add]Click the progress button of the bug to change the bug——Thanks：瓜指导、小木
3. [Modify]When creating a bug, the bear will not speak, only the bug will speak
4. [Add]Added 55 bug replies
5. [Modify]When [do not show what the bug says], only show 1 bug saying——Thanks：小木