const cookieparser = process.server ? require('cookieparser') : undefined
import * as jwt from 'jsonwebtoken';

export const state = () => {
    return {
        auth: null
    }
}
export const mutations = {
    setAuth(state, auth) {
        state.auth = auth
        state.user = jwt.decode(auth)
    }
}
export const actions = {
    nuxtServerInit({ commit }, { req }) {
        let auth = null
        if (req.headers.cookie) {
            const parsed = cookieparser.parse(req.headers.cookie)
            try {
                auth = parsed.auth
                commit('setAuth', auth)
            } catch (err) {
                // No valid cookie found
            }
        }
    }
}