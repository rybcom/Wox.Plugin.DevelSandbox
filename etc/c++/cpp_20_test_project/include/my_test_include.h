#pragma once

#include <string>
#include <sstream>
#include <vector>
#include <format>
#include <ranges>
#include <algorithm>
#include <numeric>

constexpr double GasVolumePerMol = 22.4;

struct Player
{
	std::string name;
	double score{ 0 };
};

class Team
{
public:

	Team(std::string_view name, std::initializer_list<Player> players)
	{
		_players = players;
		_name = name;
	}

	size_t getPlayersCount() const
	{
		return _players.size();
	}

	double getOverallScore() const
	{
		double sum{ 0 };
		std::ranges::for_each(_players, [&sum](auto const& player) { sum += player.score; });
		return sum;
	}

	std::string getTeamLineup() const
	{
		std::stringstream ss;
		auto printPlayer = [&ss](Player const& player)
		{
			ss << std::format("\t{}\n", player.name);
		};

		ss << std::format("[{}] Lineup :\n", _name);
		std::ranges::for_each(_players, printPlayer);
		return ss.str();
	}

private:

	std::string _name;
	std::vector<Player> _players;
};

