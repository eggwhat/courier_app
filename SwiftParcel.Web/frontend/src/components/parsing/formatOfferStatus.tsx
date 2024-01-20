const formatOfferStatus = (status: string) => {
    switch (status) {
      case 'waitingfordecision':
        return "waiting for decision";
      case 'pickedup':
        return "picked up";
      case 'cannotdeliver':
        return "Cannot deliver";
      default:
        return status; 
    }
};

export default formatOfferStatus;