import {ADD_TASK, COMPLETE_TASK, DELETE_TASK} from "../actions/taskActionTypes";
import {COMPLETED} from "../../statusTypes";

const initState = {
    tasks: []
};

function Tasks(state = initState, action) {
    switch (action.type) {
        case ADD_TASK: {
            const { task } = action.payload;
            return {tasks: [...state.tasks, task]};
        }
        case COMPLETE_TASK:{
            const { id } = action.payload;
            const tasks = state.tasks.map(task =>
                task.id === id ? {...task, status: COMPLETED} : task);
            return { tasks };
        }
        case DELETE_TASK:{
            const { id } = action.payload;
            const tasks = state.tasks.filter(task => task.id !== id);
            return { tasks };
        }
        default:
            return state;
    }
}

export default Tasks;