# load libraries and data
library(spdep)
library(classInt)

setwd("C:\\ssgs")
pr <- readShapePoly("PuertoRico.shp", proj4string=CRS("+proj=longlat +ellps=WGS84"))

pr.f <- read.csv(file="PR-farm-data.csv")
pr.nb <- read.gal("PuertoRico.GAL")
pr.listw <- nb2listw(pr.nb, style="W")
pr.listb <- nb2listw(pr.nb, style="B")

# 6.2
f.den02 <- pr.f$cuerdas_02/pr.f$area
#6.4
z.f <- scale(f.den02)
ur.c <- ifelse(pr.f$u_r==1,1,-1)

evec <- read.table("pr_evecs.txt",header=T)
EV <- evec[,-(1:2)]
sEV <- cbind(EV, ur.c * EV)
colnames(sEV) <- c(colnames(EV), 
                   paste('x1',':',colnames(EV), sep = ''))
paste('x1',':',colnames(EV), sep = '')
paste('x1',':',colnames(EV))
sv.full <- lm(z.f ~ ur.c + ., data=sEV)
sv.sf <- stepAIC(lm(z.f ~ ur.c, data=sEV), scope=list(upper=sv.full), direction="forward")
summary(sv.sf)

# The selected model in the text
sv.sf <- lm(z.f ~ ur.c + EV10 + xEV8 + EV1 + xEV1 + EV3 + EV12 + EV9, data=sEV)         
summary(sv.sf)

attach(sEV)
sv.int <- 0.0052 + 3.0909*EV1 - 1.4900*EV3 - 1.2975*EV9 + 3.0649*EV10 + 1.4365*EV12 
sv.slp <- -0.2579 + 2.3006*xEV1 + 2.4197*xEV8 
detach(sEV)

summary(sv.int)
summary(sv.slp)

pal.rb3 <- c("blue","white","red")
f3.int <- classIntervals(sv.int, n=3,   
                         style="fixed", fixedBreaks=c(min(sv.int), 
                                                      -0.6, 0.6, max(sv.int)))
cols.int <- findColours(f3.int, pal.rb3)               
plot(pr, col=cols.int)
brks <- round(f3.int$brks,4)
leg <- paste(brks[-4], brks[-1], sep=" - ")
legend("bottomright", fill= pal.rb3, legend=leg, bty="n")

f3.slp <- classIntervals(sv.slp, n=3, 
                         style="fixed", fixedBreaks=c(min(sv.slp), 
                                                      -0.4, 0.4, max(sv.slp)))
cols.slp <- findColours(f3.slp, pal.rb3)               
plot(pr, col=cols.slp)
brks <- round(f3.slp$brks,4)
leg <- paste(brks[-4], brks[-1], sep=" - ")
legend("bottomright", fill= pal.rb3, 
       legend=leg ,bty="n")

plot(sv.slp, sv.int, pch=20)
cor(sv.slp,sv.int)
