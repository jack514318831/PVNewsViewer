   M S B A M L   `   `   `  ����G  CHTMLArbeiter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:HTMLArbeiterHTMLArbeiter  5   	       HTMLArbeiter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:HTMLArbeiter     5   	   -   5          � Title$  Daten Sammlung��5   	   $	��600��6    $	��960��6-   $��14(�69    � 	Resources 5	         
cbTemplate �� 
TargetType �� Path��%]   &	        �� 5
   
   " ��6-   .R�5      �� ��6   �� $ CatName�68   5      5      .��5      � -   5        $��gridData��5       � ColumnDefinitions 5   
   �� 5      $��15 �6   6,   �� 5      $	��120 �6   �� 5      $��60 �6   6,   �� 5      $	��300 �6   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $	��160 �6   �� 5      6!    � RowDefinitions 5   
   �� 5      $��40 �6   6*   �� 5      $��45 �6   6*   �� 5      $��45 �6   6*   �� 5      $��45 �6   6*   �� 5      $��45 �6   6*   �� 5      $��40 �6   6*   �� 5       $	��200 �6   6+   �� 5!      6   �� 5"      $��40 �6   6*   .+�5$   
   �� $��1��6   $��0��6$   $��Target:��61   $��Center=�6@   $��Right=�6[   $��0q�6w   �� 5%   
   -   5        $��cbTarget��5%      $��2��6#   $��0��63   $��5��6A   $��5q�6U   ��5&      �� 5'      .R�5(      �� .+�5)      �� ��6$   �� *Name+5*      5+      5-      �� 5.   
   $��1��6   $��1��6$   $��	Catagory:��61   $	��Top=�6B   $��Right=�6Z   $��0q�6v   �� 5/   
   -   5        $��
SPCatagory��5/      
�������$��2��6>   $��1��6N   $��5��6\   $��4��6p   $��8q�6�   �� 50   
   $��1��6   $��5��6$   $��5q�61   $��Latest News:��6<   $��Right=�6P   $��20��6l   �� 51   
   -   5        $��txtNewsCount��51      $��2��6&   $��5��66   $��5q�6C   $��20��6N   �� 53   
   -   5        $��lbNewslistCat��53      $��1��6'   $��6��67   $��5q�6D   $��2��6O   54      �� 55   
   -   5        $��
lbNewslist��55      $��2��6$   $��6��64   $��5q�6A   $��2��6{   $��2��6�   56      �� 57   
   -   5        $��tbNewsContent��57      $��4��6'   $��6��67   $��5��6D   $��5q�6X   $��2��6c    �� TextWrapping$
 Wrap=�6t   $��2q�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   � 59   
   -	   5        $��	pbProcess��59       � Minimum$ 0Q�6'   	 � Maximum$		 100Q�63   $��1��6A   $��8��6Q   $��5��6^   $��
5,10,10,10q�6r   �� 5:   
   -
   5        $��
tblockText��5:      $��6��6&   $��8��66   $��10q�6C   $��12(�6O   $
��Wait��6]   �� 5<   
   -   5        $��
btnGetData��5<      $��7��6#   $��0��63   $��5,5,5,0q�6@   $��Get Data��6Q   $��30��6}   $	��Top=�6�   �� 5=   
   -   5        $��btnEdit��5=      $��11��6    $��1��61   $��5q�6>   $��
Regex Edit��6I   $��2��6t   �� 5>   
   -   5        $��btnRegexHelper��5>      $��11��6'   $��2��68   $��5q�6E   $��Regex Helper��6P   $��2��6�   �� 5?   
   -   5        $��btnGetAllCat��5?      $��5q�6%   $��11��60   $��3��6A   $��Get data of all Catagory��6N   6q   �� 5@   
   -   5        $��btnGetDataToTool��5@      $��11��6)   $��4��6:   $��5q�6G   $��Get Data to Tool��6R   6m   �� 5A   
   -   5        $��btnGetAllDataToTool��5A      $��11��6,   $��5��6=   $��5q�6J   $��Get All Data to Tool��6U   6t   �� 5B   
   -   5        $��btnNewsSelect��5B      $��11��6&   $��8��67   $��5q�6D   $��Select News��6O   6e   �� 5D   
   -   5        $��btnTest��5D      $��1��6    $��2��60   $��5,0,5,5q�6=   $
��Test��6N   $��35��6s   $��Bottom=�6   �� 5E   
   -   5        $��txtInput��5E      $��1��6"   $��3��62   $��5q�6?   �� 5F   
   -   5        $��	txtOutput��5F      $��1��6#   $��4��63   $��5q�6@   5G      5H      