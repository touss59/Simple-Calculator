   M S B A M L   `   `   `  ����E  ACalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Calculator
Calculator  5   	       Calculator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $localclr-namespace:Calculator  5   	     � Title$  
Calculator��5	   	   $	��550��6   $	��350��6)    � 	Resources 5
         numbersColor �� Color  operatorsColor  foregroundColor �� 
TargetType �� Property �� Value  numbersButtonStyle  operatorsButtonStyle �� BasedOn  additionalButtonStyle��%�  &	        &	 !     &	 @     '�� d     &	 �     7  7   &	 �    7  &	 �    7  �� 5   
   $ #333333��6/   �� 5   
   $ Green��61   �� 5   
   $ 
WhiteSmoke��62   �� 5   
   " ��6   .�5      ��  � ��$ 70(�6)   �� 5       � ��$ 0,0,10,0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � ��$ 25(�6)   �� 5       � ��$ 5q�6'   �� 5       � ��8  6+   5      �� 5   
   " ��6   8   .�5      ��  � ��
 � ���6+   5      �� 5   
   " ��6   8   .�5       ��  � ��
 �����6+   �� 5!       � ��
 �   �6+   5"      5#      .��5$      � $��10q�6    � ColumnDefinitions 5%   
   �� 5&      $��* �6   �� 5'      $��* �6   �� 5(      $��* �6   �� 5)      $��* �6    � RowDefinitions 5+   
   �� 5,      $��2* �6   �� 5-      $��* �6   �� 5.      $��* �6   �� 5/      $��* �6   �� 50      $��* �6   �� 51      $��* �6   �� 52      $��* �6   .+�54   
   �� $��0��6   $��4��6!   ���#0F5A0F$��5q�6D   �� 55   
   -   5        	 ��Name$	 resultLabel��56      $��0��55      $��16(�57      $��4��58      $��Right=�59      $��Center=�5:      �� 5;   
   -   5        $��AC��5<      #��[ 5>      $��1��5?      $��2��5@      �� 5B   
   -   5        $	��Sup��5B      #��[ 5D      $��2��5E      $��1��5F      �� 5G   
   -   5        $��/��5G      #��[ 5I      $��3��5J      $��1��5K      �� 5L   
   -   5        $��7��5L      #��[ 5N      $��2��5O      �� 5P   
   -   5        $��8��5P      #��[ 5R      $��1��5S      $��2��5T      �� 5U   
   -   5        $��9��5U      #��[ 5W      $��2��5X      $��2��5Y      �� 5Z   
   -   5        $��*��5Z      #��[ 5\      $��3��5]      $��2��5^      �� 5_   
   -	   5        $��4��5_      #��[ 5a      $��3��5b      �� 5c   
   -
   5        $��5��5c      #��[ 5e      $��1��5f      $��3��5g      �� 5h   
   -   5        $��6��5h      #��[ 5j      $��2��5k      $��3��5l      �� 5m   
   -   5        $��-��5m      #��[ 5o      $��3��5p      $��3��5q      �� 5r   
   -   5        $��3��5r      #��[ 5t      $��4��5u      �� 5v   
   -   5        $��2��5v      #��[ 5x      $��1��5y      $��4��5z      �� 5{   
   -   5        $��1��5{      #��[ 5}      $��2��5~      $��4��5      �� 5�   
   -   5        $��+��5�      #��[ 5�      $��3��5�      $��4��5�      �� 5�   
   -   5        $��0��5�      #��[ 5�      $��5��5�      $��2��5�      �� 5�   
   -   5        $��,��5�      #��[ 5�      $��2��5�      $��5��5�      �� 5�   
   -   5        $��=��5�      #��[ 5�      $��3��5�      $��5��5�      �� 5�   
   -   5        $��(��5�      #��[ 5�      $��6��5�      �� 5�   
   -   5        

 � Tag$
 false��5�      #��[ 5�      $��1��5�      $��6��5�      ��5�      0  *MaterialDesignThemes.Wpf.PackIconExtension  *ArrowLeftThick+   Size$ 25X�5�      �� 5�   
   -   5        $

 true��5�      #��[ 5�      $��2��5�      $��6��5�      ��5�        *ArrowRightThick+$ 25X�5�      �� 5�   
   -   5        $��)��5�      #��[ 5�      $��3��5�      $��6��5�      5�      5�      