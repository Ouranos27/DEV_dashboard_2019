import React from 'react';
import makeStyles from "@material-ui/core/styles/makeStyles";
import clsx from "clsx";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import {CssBaseline} from "@material-ui/core";
import Drawer from "@material-ui/core/Drawer";
import Divider from "@material-ui/core/Divider";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import FilterDramaIcon from '@material-ui/icons/FilterDrama'
import AttachMoneyIcon from '@material-ui/icons/AttachMoney';
import MovieIcon from '@material-ui/icons/Movie';
import TranslateIcon from '@material-ui/icons/Translate';
import CloudQueueIcon from '@material-ui/icons/CloudQueue';
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Paper from "@material-ui/core/Paper";
import Box from "@material-ui/core/Box";
import WeatherWidget from "./Widgets/WeatherWidget";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import TextField from "@material-ui/core/TextField";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import {ExpandLess, ExpandMore} from "@material-ui/icons";
import Collapse from "@material-ui/core/Collapse";

const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
    root: {
        display: 'flex'
    },
    toolbar: {
        paddingRight: 24, // keep right padding when drawer closed
    },
    toolbarIcon: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'flex-end',
        padding: '0 8px',
        ...theme.mixins.toolbar,
    },
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
    },
    appBarShift: {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    },
    menuButton: {
        marginRight: 0,
    },
    menuButtonHidden: {
        display: 'none',
    },
    title: {
        flexGrow: 1,
    },
    drawerPaper: {
        position: 'relative',
        whiteSpace: 'nowrap',
        width: drawerWidth,
        transition: theme.transitions.create('width', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    },
    drawerPaperClose: {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
            width: theme.spacing(9),
        },
    },
    appBarSpacer: theme.mixins.toolbar,
    content: {
        flexGrow: 1,
        height: '100vh',
        overflow: 'auto',
    },
    container: {
        paddingTop: theme.spacing(4),
        paddingBottom: theme.spacing(4),
    },
    paper: {
        padding: theme.spacing(2),
        display: 'flex',
        overflow: 'auto',
        flexDirection: 'column',
    },
    fixedHeight: {
        height: 240,
    },
    nested: {
        paddingLeft: theme.spacing(4),
    },
}));

export default function Dashboard() {
    const classes = useStyles();
    const [open, setOpen] = React.useState(true);
    const handleDrawerOpen = () => {
        setOpen(true);
    };
    const handleDrawerClose = () => {
        setOpen(false);
    };
    const fixedHeightPaper = clsx(classes.paper, classes.fixedHeight);
    let drawer;
    if (!open) {
        drawer = <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            className={clsx(classes.menuButton, open && classes.menuButtonHidden)}
        >
            <MenuIcon/>
        </IconButton>
    } else {
        drawer = <IconButton color="inherit" onClick={handleDrawerClose}><ChevronLeftIcon/></IconButton>
    }
    
    const [openWeatherList, setOpenWeatherList] = React.useState(false);
    const weatherClick = () => {
        setOpenWeatherList(!openWeatherList);
    };
    
    const [openCurrentWeather, setOpenWeather] = React.useState(false);
    const CurrentWeatherDialogOpen = () => {
        setOpenWeather(true);
    };
    const CurrentWeatherDialogClose = () => {
        setOpenWeather(false);
    };
    const [WeeklyWeatherOpen, setOpenWeeklyWeather] = React.useState(false);
    const WeeklyWeatherDialogOpen = () => {
        setOpenWeeklyWeather(true);
    };
    const WeeklyWeatherDialogClose = () => {
        setOpenWeeklyWeather(false);
    };
    
    const [openCurrency, setOpenCurrency] = React.useState(false);
    const CurrencyDialogOpen = () => {
        setOpenCurrency(true);
    };
    
    const CurrencyDialogClose = () => {
        setOpenCurrency(false);
    };
    
    let widgets = [];
    widgets.concat(<WeatherWidget city="Paris" frequency={20}/>);
    return (
        <div className={classes.root}>
            <CssBaseline/>
            <Drawer
                variant="permanent"
                classes={{
                    paper: clsx(classes.drawerPaper, !open && classes.drawerPaperClose),
                }}
                open={open}
            >
                <div className={classes.toolbarIcon}>
                    {drawer}
                </div>
                <Divider/>
                <List>
                    <div>
                        <ListItem button onClick={weatherClick}>
                            <ListItemIcon>
                                <FilterDramaIcon/>
                            </ListItemIcon>
                            <ListItemText primary="Weather"/>
                            {openWeatherList ? <ExpandLess /> : <ExpandMore />}
                        </ListItem>
                        <Collapse in={openWeatherList} timeout="auto" unmountOnExit>
                            <List component="div" disablePadding>
                                <ListItem button className={classes.nested} onClick={CurrentWeatherDialogOpen}>
                                    <ListItemIcon>
                                        <CloudQueueIcon />
                                    </ListItemIcon>
                                    <ListItemText primary="Current Weather" />
                                </ListItem>
                                <Dialog open={openCurrentWeather} onClose={CurrentWeatherDialogClose} aria-labelledby="form-dialog-title">
                                    <DialogTitle id="form-dialog-title">Current Weather</DialogTitle>
                                    <DialogContent>
                                        <DialogContentText>
                                            Please enter a city and the frequency at witch the weather data will reload
                                        </DialogContentText>
                                        <TextField autoFocus margin="dense" id="city" label="City" type="text" fullWidth/>
                                        <TextField autoFocus margin="dense" id="weatherFrequency" label="Frequency" text="text"
                                                   fullWidth/>
                                    </DialogContent>
                                    <DialogActions>
                                        <Button onClick={CurrentWeatherDialogClose} color="primary">
                                            Cancel
                                        </Button>
                                        <Button onClick={CurrentWeatherDialogClose} color="primary">
                                            Subscribe
                                        </Button>
                                    </DialogActions>
                                </Dialog>
                                <ListItem button className={classes.nested} onClick={WeeklyWeatherDialogOpen}>
                                    <ListItemIcon>
                                        <CloudQueueIcon />
                                    </ListItemIcon>
                                    <ListItemText primary="Weather over 7 days" />
                                </ListItem>
                                <Dialog open={WeeklyWeatherOpen} onClose={WeeklyWeatherDialogClose} aria-labelledby="form-dialog-title">
                                    <DialogTitle id="form-dialog-title">Weather over 7 days</DialogTitle>
                                    <DialogContent>
                                        <DialogContentText>
                                            Please enter a city and the frequency at witch the weather data will reload
                                        </DialogContentText>
                                        <TextField autoFocus margin="dense" id="city" label="City" type="text" fullWidth/>
                                        <TextField autoFocus margin="dense" id="weatherFrequency" label="Frequency" text="text"
                                                   fullWidth/>
                                    </DialogContent>
                                    <DialogActions>
                                        <Button onClick={WeeklyWeatherDialogClose} color="primary">
                                            Cancel
                                        </Button>
                                        <Button onClick={WeeklyWeatherDialogClose} color="primary">
                                            Subscribe
                                        </Button>
                                    </DialogActions>
                                </Dialog>
                            </List>
                        </Collapse>
                        <ListItem button onClick={CurrencyDialogOpen}>
                            <ListItemIcon>
                                <AttachMoneyIcon/>
                            </ListItemIcon>
                            <ListItemText primary="Currency rate"/>
                        </ListItem>
                        <Dialog open={openCurrency} onClose={CurrencyDialogClose} aria-labelledby="form-dialog-title">
                            <DialogTitle id="form-dialog-title">Currency rate</DialogTitle>
                            <DialogContent>
                                <DialogContentText>
                                    Please enter the two currencies needed
                                </DialogContentText>
                                <TextField autoFocus margin="dense" id="currecny1" label="first currency" type="text" fullWidth/>
                                <TextField autoFocus margin="dense" id="currecny2" label="second currency" type="text" fullWidth/>
                                <TextField autoFocus margin="dense" id="weatherFrequency" label="Frequency" text="text"
                                           fullWidth/>
                            </DialogContent>
                            <DialogActions>
                                <Button onClick={CurrencyDialogClose} color="primary">
                                    Cancel
                                </Button>
                                <Button onClick={CurrencyDialogClose} color="primary">
                                    Subscribe
                                </Button>
                            </DialogActions>
                        </Dialog>
                        <ListItem button>
                            <ListItemIcon>
                                <MovieIcon/>
                            </ListItemIcon>
                            <ListItemText primary="Movies"/>
                        </ListItem>
                        <Dialog open={openMovies} onClose={MoviesDialogClose} aria-labelledby="form-dialog-title">
                            <DialogTitle id="form-dialog-title">Movies info</DialogTitle>
                            <DialogContent>
                                <DialogContentText>
                                    Please enter the two currencies needed
                                </DialogContentText>
                                <TextField autoFocus margin="dense" id="currecny1" label="first currency" type="text" fullWidth/>
                                <TextField autoFocus margin="dense" id="weatherFrequency" label="Frequency" text="text"
                                           fullWidth/>
                            </DialogContent>
                            <DialogActions>
                                <Button onClick={MoviesDialogClose} color="primary">
                                    Cancel
                                </Button>
                                <Button onClick={MoviesDialogClose} color="primary">
                                    Subscribe
                                </Button>
                            </DialogActions>
                        </Dialog>
                        <ListItem button>
                            <ListItemIcon>
                                <TranslateIcon/>
                            </ListItemIcon>
                            <ListItemText primary="Translate"/>
                        </ListItem>
                        <Dialog open={openTranslate} onClose={TranslateDialogClose} aria-labelledby="form-dialog-title">
                            <DialogTitle id="form-dialog-title">Translate</DialogTitle>
                            <DialogContent>
                                <DialogContentText>
                                    Please enter the two currencies needed
                                </DialogContentText>
                                <TextField autoFocus margin="dense" id="currecny1" label="first currency" type="text" fullWidth/>
                                <TextField autoFocus margin="dense" id="weatherFrequency" label="Frequency" text="text"
                                           fullWidth/>
                            </DialogContent>
                            <DialogActions>
                                <Button onClick={MoviesDialogClose} color="primary">
                                    Cancel
                                </Button>
                                <Button onClick={MoviesDialogClose} color="primary">
                                    Subscribe
                                </Button>
                            </DialogActions>
                        </Dialog>
                    </div>
                </List>
            </Drawer>
            <main className={classes.content}>
                <di/>
                <Container maxWidth="lg">
                    <Grid container spacing={3}>
                        <WeatherWidget city="Paris" frequency={20}/>
                        {/* Chart */}
                        <Grid item xs={12} md={8} lg={9}>
                            <Paper className={fixedHeightPaper}>
                            </Paper>
                        </Grid>
                        {/* Recent Deposits */}
                        <Grid item xs={12} md={4} lg={3}>
                            <Paper className={fixedHeightPaper}>
                            </Paper>
                        </Grid>
                        {/* Recent Orders */}
                        <Grid item xs={12}>
                            <Paper className={classes.paper}>
                            </Paper>
                        </Grid>
                    </Grid>
                    <Box pt={4}>
                    </Box>
                </Container>
            </main>
        </div>
    );
}