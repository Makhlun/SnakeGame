   M S B A M L   `   `   `  ����F  BSnake_v.0.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Snake_v._0._0Snake_v._0._0  5   	       Snake_v._0._0.PlayWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Snake_v._0._0     5   	   -   5          � Title$  
Snake Game��5   	   $	��531��6   $	��800��6)    � WindowStyle$
 None=�65   $	��300��5	   	   $	��300��6    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�6      BackgroundColor#��[  5   	     	TextColor#��[ 5   	     MainFont#��[ 5   	   $��30(�5   	    � Icon$ Assets\Icon.png��5   	   .��5      (� $
��Left=�6   $	��Top=�5      .��5   
   � $	��503��6   $	��809��6    � ColumnDefinitions 5      �� 5      $
��561* �6#   �� 5      $
��231* �6#   �� 5      $	��17* �6#    � RowDefinitions 5      �� 5      $��18.607 �6    �� 5      $��85.393 �6    �� 5      .+�5       ��  �� Orientation$ 
Horizontal=�6   $��Center=�5!      $	��Top=�5"      $��2��60   .+�5#      �� -   5         ��Name$ 	ScoreText��5#      $��0 20 0 0q�5$      $��40(�5%      $��
SCORE :  0��5&      �� 5'      -   5        $	 Lvl��5'      $��40(�5(      $��	60 20 0 0q�5)      $��
LEVEL :  -��5*      5+      �� 5-      -   5        	 ��Name$	 
GridBorder��5-      $��2��5.        GridLineColor#��[ 5/      $	��1.5q�50      $��Center=�51      $��Center=�52      
 �� EdgeMode$
 Aliased=�53      $	��400��54      $	��400��6   ._�55      A� -   5         A�Name$ GameGrid��55      $	��400��56      $	��400��57        GridBackgroundColor#��[ 58       J� SnapsToDevicePixels .  �� BitmapScalingMode$ HighQuality=�5:      5;      5<      �� 5=      -   5        $	 Overlay��5=        OverlayColor#��[ 5>      $
 Aliased=�5?      $��2��66   $	��400��5@      $	��400��6"   $��Center=�5A      $��Center=�5B      ._�5C      �� -   5        $ OverlayText��5C      $��Press any KEY to START��5D      $��26(�5E      $��Center=�5F      $��Center=�5G      $
��Wrap=�5H      5I      �� 5J      $	��Top=�6   $��Center=�5K      $��0,15,0,0q�66   $��2��6H   $��1��6Y   $��1��6i   .+�5L      �� -   5         ��Name$
 Back��5L      $
��BACK��5M      $��65��5N      $	��230��6%   $	��Top=�5O      $��Center=�5P      $��0,0,0,0q�5Q   ,   �� 5R      -	   5        $ Restart��5R      $��RESTART��5S      $��65��5T      $	��230��6%   $	��Top=�5U      $��Center=�5V      $��0,50,0,0q�5W   /   �� 5X      -
   5        $ Pause��5X      $��PAUSE��5Y      $��65��5Z      $	��230��6%   $	��Top=�5[      $��Center=�5\      $��0,20,0,0q�5]   -   �� 5^      -   5        $
 Save��5^      $
��SAVE��5_      $��65��5`      $	��230��6%   $	��Top=�5a      $��Center=�5b      $��0,40,0,0q�5c   ,   �� 5d      -   5        $ RestoreB��5d      $��RESTORE��5e      $��65��5f      $	��230��6%   $	��Top=�5g      $��Center=�5h      $��0,20,0,0q�5i   /   5j      5l      5m      5n      