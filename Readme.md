# PARKING SYSTEM
Simple parking system using .NET 5 without UI (console). Every parking slot can only accomodate one car or one motorcyle
***

## COMMAND
>This application can only accept "Motor" or "Mobil" input

### INITIALIZE PARKING SLOT
`$ create_parking_lot 6` 
Initialize 6 parking slot. If previously there are already data, it will reset to new (All Null)

### PARK A VEHICLE
`$ park B-1234-XYZ Putih Mobil` 
Park the vehicle with a white car type plate number B-1234-XYZ

### UNPARK A VEHICLE
`$ leave 1` 
Unpark the vehicle in slot 1

### GET PARKING LOT STATUS
`$ status` 
Get current parking lot status

### GET COUNT OF VEHICLE BY TYPE
`$ type_of_vehicles Mobil` 
Get count(s) of vehicle "Mobil" type

`$ type_of_vehicles Motor` 
Get count(s) of vehicle "Motor" type

### GET LIST ODD REGISTRAION NUMBER
`$ registration_numbers_for_vehicles_with_ood_plate` 
Get a list of odd registration number vehicle

### GET LIST EVEN REGISTRAION NUMBER
`$ registration_numbers_for_vehicles_with_even_plate` 
Get a list of even registration number vehicle

### GET LIST REGISTRATION NUMBER BY REGISTRATION NUMBER COLOR
`$ registration_numbers_for_vehicles_with_colour Putih` 
Get a list of vehicle's registration number by it's registration number color

### GET LIST SLOT NUMBER BY REGISTRATION NUMBER COLOR
`$ slot_numbers_for_vehicles_with_colour Putih` 
Get a list of vehicle's slot number by it's registration number color

### GET SLOT NUMBER BY REGISTRATION NUMBER
`$ slot_number_for_registration_number B-3141-ZZZ` 
Get slot number of vehicle by it's registration number

### EXIT THE APPLICATION
`$ exit` 
Exit the application.