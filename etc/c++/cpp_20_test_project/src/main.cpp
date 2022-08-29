
#include <iostream>
#include "my_test_include.h"

int main()
{
	Team team{ "Kozenky64", { {"Emil",12.4}, {"Jozo",8} , { "Fero",12}}};

    std::cout <<  std::format("Mol gas volume = {:.2f} l", GasVolumePerMol);
	std::cout << "\n-------------------------------\n";
	std::cout << std::format("Team score is : {}", team.getOverallScore());
	std::cout << "\n-------------------------------\n";
	std::cout << team.getTeamLineup();

}