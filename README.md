# ArcTouch

# Guide: Getting Started With ArcTouch (TMDB Challenge)

MVVMCross project using Xamarin.Forms framework that provides several key benefits. 

- Beautifully themed UI by default, beginning with Material Design, side menu.
- Modern navigation UI
- Ability to navigate via routes (not implemented)
- Performance!
- Unified design by default


# Build Instructions

-No special build instructions

# Thrid party libraries

- MvvmCross 6.2.3 -> Base Framework
- NewtonSoft.Json 12.0.1 -> Json Serializer / Deserializer
- Xamarin.FFImageLoading 2.4.4.859 -> Image manipulation and caching
- Xamarin.Forms 3.5.0.169047 -> UI components abstraction library
- Acr.UserDialogs 7.0.3 -> Multiplatform Dialogs library


# Project structure

The project is organized in a typical MvvmCross layout:

3 Solution folders: Application, Platform and Test

- Application -> contains ArcTouch.Core project with Behaviors, Services, Constants, Converters, Helpers, Models, ViewModels.  contains ArcTouch.Ui with App.xaml, color resources file, views and pages.
This projects are .NetStandard 2.0 libraries.

- Platform -> this folder contains the Android and iOS platform projects that reference ArcTouch.Core and ArcTouch.Ui

- Test -> This folder contains a typical Xunit test project (empty) 


# Project Notes

This application is not using any key value cache or SQLite local storage to keep a local copy of TMDB API service data. For an application of this nature, that would be desirable.
Also, the splash screen and UI general look and feel was kept standard.

No UiTest project included as it is not the target of this challenge.

This application has not been performance profiled with Xamarin Profiler, and memory consumption might not be optimal.

The working executables should be obtained from the Debug / Debug iPhoneSimulator build profiles.
