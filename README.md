# ImageSquirrel
A companion application to ImageBird. Since ImageBird isn't finished, this
project is acting as a stakeholder to spur development of it.

## Intended Purpose
The intended purpose is to allow a user to categorize and view images. The
backend is designed to be modular, with the ability for plugins to add new
image sources, such as:
  * Local filesystem directories (partially complete)
  * Web services (planned)
  * Bespoke image stores

## Remaining Work
The following features are planned, but not yet implemented:
  * Image Categorization
  * Image Viewing
  * Frontend
    + There's currently a console application frontend, but this is only a
      development aid.

# Getting Started
There are currently no prebuilt binaries available. The solution can be built
using Visual Studio Community 2017.

## Plugin Development
There is currently no well-defined ABI. It is recommended against writing a
plugin at this time.