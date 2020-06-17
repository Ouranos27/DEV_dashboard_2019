import React, {Component} from "react";
import Grid from "@material-ui/core/Grid";
import {Paper} from "@material-ui/core";
import Typography from "@material-ui/core/Typography";
import authService from "../api-authorization/AuthorizeService";

export default class WeatherWidget extends Component {
    constructor(props) {
        super(props);
        this.state = {
            weatherData: [],
            weatherLoading: true,
            time: 0,
            isOn: false,
            start: 0
        };
        
        this.startTimer = this.startTimer.bind(this);
        this.stopTimer = this.stopTimer.bind(this);
        this.resetTimer = this.resetTimer.bind(this);
    }

    startTimer() {
        this.setState({
            isOn: true,
            time: this.state.time,
            start: Date.now() - this.state.time
        });
        this.timer = setInterval(() => this.setState({
            time: Date.now() - this.state.start
        }), 1);
    }
    stopTimer() {
        this.setState({isOn: false});
        clearInterval(this.timer)
    }
    resetTimer() {
        this.setState({time: 0, isOn: false})
    }

    componentDidMount() {
        this.populateWeatherData(this.props.city);
        this.startTimer();
    }
    
    async populateWeatherData(city) {
        const token = await authService.getAccessToken();
        const response = await fetch('weatherforecast/now?city=' + city, {
            headers: !token ? {} : {'Authorization': `Bearer ${token}`}
        });
        const data = await response.json();
        this.setState({weatherData: data, weatherLoading: false});
    }

    render() {
        let style = {
          height: 240  
        };
        let flex = {
            display: 'flex',
            flexWrap: 'wrap'
        };
        if (this.state.time === this.props.frequency) {
            this.stopTimer();
            this.resetTimer();
            this.populateWeatherData(this.props.city);
            this.startTimer();
        }
        return (
            <React.Fragment>
                <Grid item xs={12} md={4} lg={5}>
                    <Paper className={style}>
                        <Typography variant="body1">
                            {!this.state.weatherLoading && this.state.weatherData.city}
                        </Typography>
                        <Typography variant="body2">
                            {!this.state.weatherLoading && this.state.weatherData.date}
                        </Typography>
                        <Typography variant="body2">
                            {!this.state.weatherLoading && this.state.weatherData.summary}
                        </Typography>
                        <div className={flex}>
                            <div style={{
                                marginRight: 32
                            }}>
                                <div className={flex}>
                                    <img src={!this.state.weatherLoading && this.state.weatherData.icon} alt="weather icon"/>
                                </div>
                                <Typography variant="h4">
                                    {!this.state.weatherLoading && this.state.weatherData.temperatureC} °C
                                </Typography>
                                <Typography variant="h4">
                                    {!this.state.weatherLoading && this.state.weatherData.temperatureF} °F
                                </Typography>
                            </div>
                        </div>
                    </Paper>
                </Grid>
            </React.Fragment>
        );
    }
}