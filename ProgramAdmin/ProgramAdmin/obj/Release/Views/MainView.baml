   M S B A M L   `   `   `  ���� G  CProgramAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ProgramAdmin.ConvertersProgramAdmin.Converters  !    ProgramAdmin.Views.MainView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     k gTelerik.Windows.Controls.Input, Version=2016.1.112.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2016.1.112.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2016.1.112.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2016.1.112.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Etelerik1http://schemas.telerik.com/2008/xaml/presentation     3c%clr-namespace:ProgramAdmin.Converters     -     � Title$  Program Admin��$	��300��$	��300�� � WindowState$ 	Maximized=� � Icon$1 +/ProgramAdmin;component/Resources/admin.ico��.��/  )Telerik.Windows.Controls.RadBusyIndicator     IsBusy �� *IsBusy+ �� UpdateSourceTrigger$ PropertyChanged=�.���  � 	Resources 4  .ProgramAdmin.Converters.BoolToVisibleConverter   BoolToVisConverter;  5ProgramAdmin.Converters.InverseBoolToVisibleConverter  InverseBoolToVisConverter��%   &	        &	          .+��� .@��� $��Programs��.��� .+�� y��� *AddingProgram+ �� 	Converter# [  � RowDefinitions �� $��80 ��� $��1* �.+���  �� Orientation$ 
Horizontal=�.+�(  "Telerik.Windows.Controls.RadButton  $��Display Programs��$��10,10,10,10q����� *GetProgramsCommand+  $��Add A Program��$��10,10,10,10q����� *AddProgramCommand+  $��Edit:��$��10,10,10,10q����� *EditProgramInformationCommand+*  $Telerik.Windows.Controls.RadComboBox  	 �� DisplayMemberPath$	 Value��$��
0,10,10,10q�$	��100������ *ProgramList+$ PropertyChanged=�
 �� SelectedItem
 �� *EditId+*  $Telerik.Windows.Controls.RadGridView  ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   GridLinesVisibility$
 Both=�$��1��*  $Telerik.Windows.Controls.DataControl   ItemsSource �� *
Programs+�� y��� *AddingProgram+# [  .+��  � ColumnDefinitions �� $	��125 ��� $	��300 � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��Program Name*:��$��0��$��0��$��Right=�$��10,10,10,10q��� $��
Language*:��$��0��$��1��$��Right=�$��10,10,10,10q��� $��Path*:��$��0��$��2��$��Right=�$��10,10,10,10q��� $��	Comments:��$��0��$��3��$��Right=�$��10,10,10,10q��� $��Primary User(s)*:��$��0��$��4��$��Right=�$��10,10,10,10q��� $��Status*:��$��0��$��5��$��Right=�$��10,10,10,10q��� $��Notes:��$��0��$��6��$��Right=�$��10,10,10,10q��� $��Developer(s)*:��$��0��$��7��$��Right=�$��10,10,10,10q��� $��In Production?*:��$��0��$��8��$��Right=�$��10,10,10,10q��� $��1��$��0��$��10,10,10,10q����� *Program.ProgramName+ �� 
IsReadOnly �� *	Editing+�� $��1��$��1��$��10,10,10,10q����� *Program.Language+ �� *	Editing+�� $��1��$��2��$��10,10,10,10q����� *Program.Path+ �� *	Editing+�� $��1��$��3��$��10,10,10,10q����� *Program.Comments+�� $��1��$��4��$��10,10,10,10q����� *Program.PrimaryUser+�� $��1��$��5��$��10,10,10,10q����� *Program.Status+�� $��1��$��6��$��10,10,10,10q����� *Program.Notes+�� $��1��$��7��$��10,10,10,10q����� *Program.Developer+�� $��1��$��8��$��10,10,10,10q� d� 	IsChecked �� *Program.InProduction+�� $>��8All fields marked with an asterisk(*) must be filled in.��
���  ��
�������$	��425�� �� TextAlignment$ Center=�$
��Left=�y��� *ShowMessage+# [  �� $ 
Horizontal=�.+�  $��10,10,10,10q����� *AddProgramMessage+���� *ProgramAddedCommand+  $��Cancel��$��10,10,10,10q����� *ProgramCancelledCommand+�� $��Program Logs��.���  �� $��80 ��� $��1* �.+�� $��0�� �� $	��150 ��� $	��150 ��� $	��150 ��� $	��300 �.+�  $��Display All Logs��$��10,10,10,10q�$	��125��$
��Left=�$��0������ *GetLogsCommand+� $��1�� �� $��* ��� $��* �.+�  $	 Value��$��0������ *LogPrograms+$ PropertyChanged=�
 �� *LogProgramId+  $��Get Program Logs��$��1������ *GetLogsCommand+� $��2�� �� $��* ��� $��* �.+�  $��0������ *
UserList+$ PropertyChanged=�
 �� *SelectedUser+  $��Get User's Logs��$��1������ *GetUserLogsCommand+� $��3�� �� $��* ��� $��* �.+�� $��0�� �� $��* ��� $��* �.+�,	  &Telerik.Windows.Controls.RadDatePicker	  $��0��0
  *Telerik.Windows.Controls.RadDateTimePicker 
  SelectedValue �� *	StartDate+	  $��1�� �� *	EndDate+  $��Get Logs By Date Range��$��1������ *GetLogsByDateCommand+  $
 Both=�$��1�� �� *ProgramLogs+�� $��Program Errors��.���  �� $��80 ��� $��1* �.+�  $��Display Errors��$��10,10,10,10q�$	��125��$
��Left=�$��0������ *GetErrorsCommand+  $
 Both=�$��1�� �� *ProgramErrors+�� $��Release Notes��.���  �� $��80 ��� $��1* �.+��� $��0��$ 
Horizontal=�.+�  $	 Value��$
��Left=�$��
0,10,10,10q�$	��300������ *ProgramList+$ PropertyChanged=�
 �� *ReleaseNoteId+�� $��50��$��Center=�.+��� $��Major��$��Center=���  �� TextAlignment$ Center=����� *ReleaseNote.Major+�� $��50��$��Center=�.+��� $��Minor��$��Center=��� $ Center=����� *ReleaseNote.Minor+�� $��50��$��Center=�.+��� $��Build��$��Center=��� $ Center=����� *ReleaseNote.Build+�� $��50��$��Center=�.+��� $��Revision��$��Center=��� $ Center=����� *ReleaseNote.Revision+�� $	��400��$��Center=�.+��� $��Notes��$��Center=��� $
 Left=����� *ReleaseNote.Notes+  $��Add Release Note��$��10,10,10,10q�$	��150��$
��Left=����� *AddReleaseNoteCommand+  $
 Both=�$��1�� �� *Notes+