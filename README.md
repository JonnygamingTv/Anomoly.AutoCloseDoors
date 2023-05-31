# Anomoly.AutoCloseDoors
A RocketMod Unturned plugin that automatically closes doors after a set amount of time.

## Configuration
- `AllowAutoCloseUnclaimedDoors` - Whether or not to allow doors to be automatically closed for wooden doors.
- `CancelOnPlayerDead` - Whether or not to cancel the door closing if the player dies.
- `CloseDelay` - The amount of time in seconds to wait before closing the door.
- `DefaultEnabled` - Whether or not the plugin is enabled by default.
- `DisplayDoorClosedMessage` - Whether or not to display a message when a door is closed.
- `MessageColor` - The color of the message when a door is closed
```xml
<?xml version="1.0" encoding="utf-8"?>
<AutoCloseDoorsPluginConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <AllowAutoCloseUnclaimedDoors>false</AllowAutoCloseUnclaimedDoors>
  <CancelOnPlayerDead>true</CancelOnPlayerDead>
  <CloseDelay>5</CloseDelay>
  <DefaultEnabled>false</DefaultEnabled>
  <DisplayDoorClosedMessage>true</DisplayDoorClosedMessage>
  <MessageColor>red</MessageColor>
</AutoCloseDoorsPluginConfiguration>
```

## Translations
- `command_autodoor` - The message displayed when the auto door command is used.
- `autodoor_closed` - The message displayed when a door is closed.
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="command_autodoor" Value="[AutoDoor] Toggled auto door: {0}" />
  <Translation Id="autodoor_closed" Value="[AutoDoor] Door has been closed." />
</Translations>
```
