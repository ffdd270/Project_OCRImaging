   M S B A M L   `   `   `  ����K  GTranslationImage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TranslationImageTranslationImage  5   	   !    TranslationImage.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TranslationImage     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 
MainWindow��5	   	   $��677.5��6   $
��1119��6+   .��5
      � $��0,0,2,1q�6    � RowDefinitions 5   
   �� 5      $��0* �6   �� 5      .+�5   
   �� -   5         ��Name$ 
MainCanvas��5      $
��Left=�6&   $	��646��6A   $	��Top=�6N   $
��1109��6f   $��2��6s   .+�5      �� -   5         ��Name$ TranslateOutput��5      $
��Left=�6/   $��88��6J    �� TextWrapping$
 Wrap=�6V   $��TranslateOutput��6j   $	��Top=�6�   $	��362��6�    �� Left$	 472��6�   
 �� Top$	 422��6�   �� 5      -   5        $ TranslateInput��5      $
��Left=�6.   $��78��6I   $
 Wrap=�6U   $��TextBox��6i   $	��Top=�6x   $	��362��6�   $ 28��6�   $	 422��6�   �� 5      -   5         ��Name$ TranslateButton��5      $��	Translate��6.   $
��Left=�6B   $	��Top=�6]   $��76��6u   	 J� RenderTransformOrigin$	 
0.428,0.95&�6�   $	 650��6�   $	 521��6�   �� 5      -   5        $ JsonOpenButton��5      $��OpenJson��6-   $
��Left=�6@   $	��Top=�6[   $��76��6s   $	 -0.453,9.65&�6�   $	 758��6�   $	 521��6�   �� 5      -   5        ���	#333737DA$	��358��5   )   
 J� 	AllowDrop
 . $ 28��6�   
���   �$ 31��6�   $	��806��6�   $��0��6�   �� 5      -   5         ��Name$ 
InputImage��5      $
��Left=�6I   
 . $	��354��5   n   $	��Top=�6{   $	��806��6�   $	 0.505,0.525&�6�    J� OpacityMask
 �����$ 28��6�   $ 31��6�   �� 5      -   5         ��Name$ OCRLangugeSelect��5      $	 395��61   $	��120��6B   $	 0.058,-0.25&�6N   $ 28��6�   .@�5      �� $��English��6   $
��Left=�61   $	��118��6L   �� 5      $��Japen��6   $
��Left=�6/   $	��118��6J   �� 5      $��Korean��6   $
��Left=�60   $	��118��6K   5      �� 5      -	   5        $ OutputLanguge��5      $	 472��6.   $	 395��6@   $	��120��6Q   .@�5      �� $��Korean��6   $
��Left=�60   $	��118��6K   �� 5      $��Japenese��6   $
��Left=�62   $	��118��6M   �� 5       $��English��6   $
��Left=�61   $	��118��6L   5!      �� 5"      -
   5         ��Name$ 	ImageList��5"      $	��510��6)   $	 858��66   $ 31��6H   $	��230��6X   
 . 5#      �� 5$      $��이미지 파일들��6   $	 858��6&   $	 0.496,0.603&�68   $ 5��6\   �� 5%      $��이미지 처리��6   $ 28��6%   $	 
0.52,0.212&�66   $ 5��6Y   5&      5'      5(      