# AcMsbEditor
A simplistic editor for Armored Core MSB map files.  

# Features
Can add new msb param entries.  
Can delete msb param entries.  
Can duplicate msb param entries in place.  

# Supported Games
Armored Core 4

# Limitations
This editor currently only supports Armored Core 4 MSB map files.  
The editor cannot edit the properties of msb param entries yet.  

The editor can duplicate, but cannot copy and paste.  
This may lead to situations where desired optional data cannot be copied or made in a map.  

The editor can only select one msb param entry at a time.  
This is due to a limitation with WinForm's TreeView control used to display them.  

The editor cannot set instance specific properties such as layer IDs or part model names.  
This means it cannot ensure they are unique either.  
This is due to it not being able to edit properties,  
and the additional workload it would take to map them all.  

The editor cannot sure totally valid names for msb param entries.  
This may be added later, it would take more mapping.  

The ability to create new msb param entries is slow due to UI difficulties.  
Currently a few prefab windows popup dialogs will show for it.  

A "save all" and "close all" feature is not yet added to the UI.  
This is minor and may be added later.  

A "delete all" feature is not yet added for msb param entries.  
A rename feature is not yet added for msb param entries.  
A version edit feature is not yet added for msb param files.  

Config options for splitter distances are not yet added.  
This is minor and may be added later.  

There may be other limitations not yet listed.

# Building
This project requires the following libraries to be cloned alongside it.  
Place them in the same top-level folder as this project.  
```
git clone https://github.com/soulsmods/SoulsFormatsNEXT.git
```

Dependencies are subject to change.