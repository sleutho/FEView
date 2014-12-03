FEView
=======

CAD viewer developed to complete a University course CAD numerical methods assignment. 
This project does not use OpenGL.

##Usage:
```
FEViewConsole input.FEM output.png
```

##Limitation:
Since there is not z buffer currently, the output does not look correct due to false depth painting order, in which further away z content can be painted above closer geometry.

##Example: Beetle

[Input](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/VW_Kaefer.FEM) -> [Output](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/VW_Kaefer.png)

![](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/VW_Kaefer.png)


##Example: Balkon

[Input](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/balkon_dreieck_III.FEM) -> [Output](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/balkon_dreieck_III.png)

![](https://raw.githubusercontent.com/sleutho/FEView/master/FEViewExample/balkon_dreieck_III.png)
