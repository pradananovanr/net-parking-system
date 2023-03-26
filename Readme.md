# PARKING SYSTEM
Simple parking system using .NET without UI (console). Every parking slot can only accomodate one car or one motorcyle
***

## COMMAND
>This application can only accept "Motor" or "Mobil" input

`$ create_parking_lot 6` 
Initialize 6 parking slot. If previously there are already data, it will reset to new (All Null)

`$ park B-1234-XYZ Putih Mobil` 
Park the vehicle with a white car type plate number B-1234-XYZ

`$ leave 1` 
Unpark the vehicle in slot 1

`$ status` 
Get current parking lot status

`$ type_of_vehicles Mobil` 
Get count(s) of vehicle "Mobil" type

`$ registration_numbers_for_vehicles_with_ood_plate` 
Get a list of odd registration number vehicle

`$ registration_numbers_for_vehicles_with_even_plate` 
Get a list of even registration number vehicle

`$ registration_numbers_for_vehicles_with_colour Putih` 
Get a list of vehicle's registration number by it's registration number color

`$ slot_numbers_for_vehicles_with_colour Putih` 
Get a list of vehicle's slot number by it's registration number color

`$ slot_number_for_registration_number B-3141-ZZZ` 
Get slot number of vehicle by it's registration number

`$ exit` 
Exit the application.