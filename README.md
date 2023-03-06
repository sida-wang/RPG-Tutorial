# RPG-Tutorial

Following [Scotts Open Source C# RPG Tutorial](https://soscsrpg.com/) while implementing improved syntaxes where possible which have been introduced in later versions of C#. 

Things I have learnt so far while following the tutorial:
* Factory design pattern for instantiating objects - this enables easier modification of how instances of a given class are constructed at a project level and is a great use of `static` classes.
* The target source of a data binding must be an object property as opposed to a member.
* A deeper understanding of data bindings, delegates and events - prompted by my confusion of how `INotifyPropertyChanged`, I spent a couple of hours learning how delegates and events work.

Things I am yet to figure out:
* An ideal way to data bind images: in [lesson 4.1](https://soscsrpg.com/build-a-c-wpf-rpg/lesson-04-1-creating-the-location-class/), the use of the ImageName property seems to be slightly different in .NET Core which requires a few changes as mentioned by a note in the link. Upon introducing those changes, I was getting quite a few warnings and decided to wait until lesson 14.3 where the loading method for images is going to be revised.
