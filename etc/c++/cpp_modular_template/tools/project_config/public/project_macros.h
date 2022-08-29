#pragma once

#define OUT

#define DEPRECATED

#define PERFORMANCE_ATTENTION 

#define MEMORY_ATTENTION 

#define CPP_IMPL

#define pure_virtual virtual

#define note(my_note) 

#define ONLY_FOR_TESTING_MODE()


#define DISABLE_WARNING_PUSH()           __pragma(warning( push ))
#define DISABLE_WARNING_POP()            __pragma(warning( pop )) 
#define DISABLE_WARNING(warningNumber)   __pragma(warning( disable : warningNumber ))

#define DISABLE_WARNING_UNREFERENCED_FORMAL_PARAMETER()    DISABLE_WARNING(4100)
#define DISABLE_WARNING_UNREFERENCED_FUNCTION()            DISABLE_WARNING(4505)
#define DISABLE_WARNING_DEPRECATED_MEMBERS()			   DISABLE_WARNING(4996)
#define DISABLE_WARNING_WCHAR_TOCHAR()					   DISABLE_WARNING(4244)
#define DISABLE_WARNING_NEW_NON_MEMBER()				   DISABLE_WARNING(4595)
#define DISABLE_WARNING_UNREFERENCED_LOCAL_VARIABLE()      DISABLE_WARNING(4101)
#define DISABLE_WARNING_POINTER_TRUNCATION()			   DISABLE_WARNING(4311)
#define DISABLE_WARNING_POINTER_TRUNCATION_UINT()		   DISABLE_WARNING(4302)
#define DISABLE_WARNING_POSSIBLE_LOS_OF_DATA()		       DISABLE_WARNING(4267)



