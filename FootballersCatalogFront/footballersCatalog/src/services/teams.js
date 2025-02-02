import axios from "axios"

export const fetchTeams = async () => {
    try {
        const response = await axios.get("http://localhost:5194/api/teams/getTeams");
        const teams = response.data;

        const formattedTeams = teams.map(team => ({
            value: team.name,
            label: team.name
        }));
        return formattedTeams;
    } catch (e) {
        console.error("Error fetching teams:", e);
    }
};